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
    public class ActionDefinitionController : ControllerBase
    {
        private readonly IAppUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IAuthService service;

        public ActionDefinitionController(IAppUnitOfWork uow, IMapper mapper, IAuthService service)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<ActionDefinitionDTO> Get()
        {
            var actionDefinitions = uow.ActionDefinitionRepository.GetAll().ToList();
            var dtos = mapper.Map<IEnumerable<ActionDefinitionDTO>>(actionDefinitions);
            return dtos;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionDefinitionDTO> Get(int id)
        {
            var action = await uow.ActionDefinitionRepository.GetById(id);
            var dto = mapper.Map<ActionDefinitionDTO>(action);
            return dto;
        }

        //// POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

    }
}
