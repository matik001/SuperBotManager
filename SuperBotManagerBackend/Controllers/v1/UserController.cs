using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    public class UserController : ControllerBase
    {
        private readonly IAppUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IAuthService service;

        public UserController(IAppUnitOfWork uow, IMapper mapper, IAuthService service)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.service = service;
        }

    }
}
