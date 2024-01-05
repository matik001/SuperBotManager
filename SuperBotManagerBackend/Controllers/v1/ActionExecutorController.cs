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

namespace SuperBotManagerBackend.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ActionExecutorController : ControllerBase
    {
        private readonly IAppUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IAuthService service;

        public ActionExecutorController(IAppUnitOfWork uow, IMapper mapper, IAuthService service)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<ActionExecutorDTO> Get()
        {
            var actionDefinitions = uow.ActionExecutorRepository.GetAll().ToList();
            var dtos = mapper.Map<IEnumerable<ActionExecutorDTO>>(actionDefinitions);
            return dtos;
        }

        //// GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionExecutorDTO> Get(int id)
        {

            var actionExecutor = await uow.ActionExecutorRepository.GetById(id);
            var dto = mapper.Map<ActionExecutorDTO>(actionExecutor);
            return dto;
        }

        //// POST api/<ValuesController>
        [HttpPost]
        public async Task Post([FromBody] ActionExecutorCreateDTO dto)
        {
            var actionExecutor = mapper.Map<ActionExecutor>(dto);
            if(actionExecutor == null)
                throw ServiceUtils.BadRequest("Bad ActionExecutorCreateDTO format");
            await uow.ActionExecutorRepository.Create(actionExecutor);
            await uow.SaveChangesAsync();
        }

        [HttpPut]
        public async Task Put(int id, [FromBody] ActionExecutorCreateDTO dto)
        {
            var action = await uow.ActionExecutorRepository.GetById(id);
            mapper.Map(dto, action);
            await uow.ActionExecutorRepository.Update(action);
            await uow.SaveChangesAsync();
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await uow.ActionExecutorRepository.Delete(id);
            await uow.SaveChangesAsync();
        }


    }
}
