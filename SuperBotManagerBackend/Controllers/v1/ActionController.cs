using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SuperBotManagerBackend.Configuration;
using SuperBotManagerBackend.DB;
using SuperBotManagerBackend.DB.Repositories;
using SuperBotManagerBackend.DTOs;
using SuperBotManagerBackend.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Action = SuperBotManagerBackend.DB.Repositories.Action;

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
            var actionDefinitions = uow.ActionRepository.GetAll().ToList();
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

        //// POST api/<ValuesController>
        [HttpPost]
        public async Task Post([FromBody] ActionCreateDTO dto)
        {
            var action = mapper.Map<Action>(dto);
            if(action == null)
                throw ServiceUtils.BadRequest("Bad ActionDTO format");
            await uow.ActionRepository.Create(action);
            await uow.SaveChangesAsync();
        }

        [HttpPut]
        public async Task Put(int id, [FromBody] ActionCreateDTO dto)
        {
            var action = await uow.ActionRepository.GetById(id);
            mapper.Map(dto, action);
            await uow.ActionRepository.Update(action);
            await uow.SaveChangesAsync();
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await uow.ActionRepository.Delete(id);
            await uow.SaveChangesAsync();
        }


    }
}
