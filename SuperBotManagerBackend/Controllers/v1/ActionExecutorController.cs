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
        private readonly IActionProducer actionProducer;

        public ActionExecutorController(IAppUnitOfWork uow, IMapper mapper, IActionProducer actionProducer)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.actionProducer = actionProducer;
        }

        [HttpGet]
        public IEnumerable<ActionExecutorDTO> Get()
        {
            var actionDefinitions = uow.ActionExecutorRepository.GetAll().Include(a => a.ActionDefinition).ToList();
            var dtos = mapper.Map<IEnumerable<ActionExecutorExtendedDTO>>(actionDefinitions);

            foreach(var dto in dtos)
                dto.ActionData.MaskSecrets(dto.ActionDefinition.ActionDataSchema);

            return dtos;
        }

        //// GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionExecutorDTO> Get(int id)
        {

            var actionExecutor = await uow.ActionExecutorRepository.GetById(id, a => a.Include(x => x.ActionDefinition));
            var dto = mapper.Map<ActionExecutorExtendedDTO>(actionExecutor);
            dto.ActionData.MaskSecrets(dto.ActionDefinition.ActionDataSchema);
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
            await actionExecutor.ActionData.EncryptNotMaskedSecrets(actionExecutor.ActionDefinition.ActionDataSchema, uow);
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
            /// encrypts secret changes and replaces them with secret guid
            await executor.ActionData.EncryptNotMaskedSecrets(executor.ActionDefinition.ActionDataSchema, uow);
            /// replaces what wasn't changed (secret masks) and replaces it with secret guid (from db)
            await executor.ActionData.ReverseMasking(originalFromDb, executor.ActionDefinition.ActionDataSchema);

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
            var executor = await uow.ActionExecutorRepository.GetById(id, a => a.Include(x => x.ActionDefinition));
            var executorData = executor.ActionData.DeepClone();
            await executorData.DecryptSecrets(executor.ActionDefinition.ActionDataSchema, uow);

            if(!executor.IsValid)
            {
                throw HttpUtilsService.BadRequest("Executor is not valid, so cannot be run");
            }
            var newActions = executorData.Inputs.Select(input =>
            {
                var action = new Action()
                {
                    ActionExecutorId = executor.Id,
                    ActionStatus = ActionStatus.Pending,
                    ActionData = new ActionSchema()
                    {
                        Input = input,
                        Output = new Dictionary<string, string>()
                    },
                };

                return action;
            }).ToList();
            foreach(var action in newActions)
            {
                await uow.ActionRepository.Create(action);
            }
            executor.LastRunDate = DateTime.UtcNow;
            await uow.ActionExecutorRepository.Update(executor);
            await uow.SaveChangesAsync();

            foreach(var action in newActions)
            {
                action.ActionExecutor = executor;
                actionProducer.SendToExecute(action);
            }

        }

    }
}
