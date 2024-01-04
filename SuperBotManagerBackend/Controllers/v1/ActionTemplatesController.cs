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
    public class ActionTemplatesController : ControllerBase
    {
        private readonly IAppUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IAuthService service;

        public ActionTemplatesController(IAppUnitOfWork uow, IMapper mapper, IAuthService service)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet]
        public IEnumerable<ActionTemplateDTO> Get()
        {
            var actionDefinitions = uow.ActionTemplateRepository.GetAll().ToList();
            var dtos = mapper.Map<IEnumerable<ActionTemplateDTO>>(actionDefinitions);
            return dtos;
        }

        //// GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionTemplateDTO> Get(int id)
        {

            var actionTemplate = await uow.ActionTemplateRepository.GetById(id);
            var dto = mapper.Map<ActionTemplateDTO>(actionTemplate);
            return dto;
        }

        //// POST api/<ValuesController>
        [HttpPost]
        public async Task Post([FromBody] ActionTemplateCreateDTO dto)
        {
            var actionTemplate = mapper.Map<ActionTemplate>(dto);
            if(actionTemplate == null)
                throw ServiceUtils.BadRequest("Bad ActionTemplateCreateDTO format");
            await uow.ActionTemplateRepository.Create(actionTemplate);
            await uow.SaveChangesAsync();
        }

        [HttpPost]
        public async Task Put(int id, [FromBody] ActionTemplateCreateDTO dto)
        {
            var action = await uow.ActionTemplateRepository.GetById(id);
            mapper.Map(dto, action);
            await uow.ActionTemplateRepository.Update(action);
            await uow.SaveChangesAsync();
        }

        [HttpPost]
        public async Task Delete(int id)
        {
            await uow.ActionTemplateRepository.Delete(id);
            await uow.SaveChangesAsync();
        }


    }
}
