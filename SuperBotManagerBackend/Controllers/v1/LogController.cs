using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperBotManagerBackend.DTOs;
using SuperBotManagerBackend.Services;
using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.Services;
using Microsoft.EntityFrameworkCore;

namespace SuperBotManagerBackend.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly IAppUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly ILogService<LogController> logService;

        public LogController(IAppUnitOfWork uow, IMapper mapper, ILogService<LogController> logService)
        {
            this.uow = uow;
            this.mapper = mapper;
            this.logService = logService;
        }

        [HttpGet]
        public IEnumerable<LogDTO> Get()
        {
            var actionDefinitions = uow.LogRepository
                                    .GetAll()
                                    .Include(a=>a.User)
                                    .OrderByDescending(a => a.ModifiedDate)
                                    .ToList();

            var dtos = mapper.Map<IEnumerable<LogDTO>>(actionDefinitions);

            return dtos;
        }

        [HttpGet("{id}")]
        public async Task<LogDTO> Get(int id)
        {

            var actionExecutor = await uow.LogRepository.GetById(id);
            var dto = mapper.Map<LogDTO>(actionExecutor);

            return dto;
        }

        [HttpPost]
        public async Task Post([FromBody] LogCreateDTO dto)
        {
            var log = mapper.Map<Log>(dto);

            await uow.LogRepository.Create(log);
            await uow.SaveChangesAsync();
        }
    }
}
