using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SuperBotManagerBackend.DTOs;
using SuperBotManagerBackend.Services;
using SuperBotManagerBase.Configuration;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperBotManagerBackend.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAppUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IAuthService service;

        public AuthController(IAppUnitOfWork uow, IMapper mapper, IAuthService service)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.service = service;
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        [ProducesResponseType<UserTokensDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<UserTokensDTO> SignIn(UserSignInRequestDTO userDto)
        {
            return await service.SignIn(userDto);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        [ProducesResponseType<UserTokensDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<UserTokensDTO> SignUp(UserSignUpRequestDTO userDto)
        {
            return await service.SignUp(userDto);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task Logout()
        {
            await service.Logout();
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType<UserTokensDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<UserTokensDTO> RefreshToken(RefreshTokenRequstDTO refreshToken)
        {
            return await service.RefreshToken(refreshToken);
        }
    }
}
