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
using Action = SuperBotManagerBase.DB.Repositories.Action;

namespace SuperBotManagerBackend.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ActionController : ControllerBase
    {
        private readonly IAppUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IAuthService service;

        public ActionController(IAppUnitOfWork uow, IMapper mapper, IAuthService service)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<ActionDTO> Get()
        {
            var actionDefinitions = uow.ActionRepository.GetAll().OrderByDescending(a=>a.ModifiedDate).ToList();
            var dtos = mapper.Map<IEnumerable<ActionDTO>>(actionDefinitions);
            return dtos;
        }

        //// GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionDTO> Get(int id)
        {
            var actionExecutor = await uow.ActionRepository.GetById(id);
            var dto = mapper.Map<ActionDTO>(actionExecutor);
            return dto;
        }

      
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ActionUpdateDTO dto)
        {
            var action = await uow.ActionRepository.GetById(id);
            mapper.Map(dto, action);
            await uow.ActionRepository.Update(action);
            await uow.SaveChangesAsync();
        }



    }
}
