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
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Action = SuperBotManagerBase.DB.Repositories.Action;

namespace SuperBotManagerBackend.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ActionExecutorController : ControllerBase
    {
        private readonly IAppUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IActionService actionService;

        public ActionExecutorController(IAppUnitOfWork uow, IMapper mapper, IActionService actionService)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.actionService = actionService;
        }

        [HttpGet]
        public IEnumerable<ActionExecutorDTO> Get()
        {
            var actionDefinitions = uow.ActionExecutorRepository
                                    .GetAll()
                                    .Include(a => a.ActionDefinition)
                                    .OrderByDescending(a=>a.ModifiedDate)
                                    .ToList();
            var dtos = mapper.Map<IEnumerable<ActionExecutorExtendedDTO>>(actionDefinitions);

            return dtos;
        }

        //// GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionExecutorDTO> Get(int id)
        {

            var actionExecutor = await uow.ActionExecutorRepository.GetById(id, a => a.Include(x => x.ActionDefinition));
            var dto = mapper.Map<ActionExecutorExtendedDTO>(actionExecutor);

            return dto;
        }

        //// POST api/<ValuesController>
        [HttpPost]
        public async Task Post([FromBody] ActionExecutorCreateDTO dto)
        {
            var actionExecutor = mapper.Map<ActionExecutor>(dto);
            if(actionExecutor == null)
                throw HttpUtilsService.BadRequest("Bad ActionExecutorCreateDTO format");

            await uow.ActionExecutorRepository.LoadDefinition(actionExecutor);
            actionExecutor.UpdateIsValid();
            await actionExecutor.ActionData.EncryptSecrets(actionExecutor.ActionDefinition.ActionDataSchema, uow);
            actionExecutor.ActionDefinition = null;

            await uow.ActionExecutorRepository.Create(actionExecutor);
            await uow.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ActionExecutorCreateDTO dto)
        {
            var executor = await uow.ActionExecutorRepository.GetById(id, a => a.Include(x => x.ActionDefinition));
            var originalFromDb = executor.ActionData.DeepClone();

            mapper.Map(dto, executor);

            executor.UpdateIsValid();
            await executor.ActionData.EncryptSecrets(executor.ActionDefinition.ActionDataSchema, uow);

            executor.ActionDefinition = null;

            await uow.ActionExecutorRepository.Update(executor);
            await uow.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await uow.ActionExecutorRepository.Delete(id);
            await uow.SaveChangesAsync();
        }


        [HttpPost("{id}/execute")]
        public async Task Execute(int id)
        {
            await actionService.Execute(id);
        }

    }
}
