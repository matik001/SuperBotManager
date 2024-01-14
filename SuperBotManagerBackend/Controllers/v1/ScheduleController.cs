using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperBotManagerBackend.DTOs;
using SuperBotManagerBackend.Services;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.Services;


namespace SuperBotManagerBackend.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IAppUnitOfWork uow;
        private readonly IMapper mapper;
        //private readonly IScheduleService scheduleService;

        public ScheduleController(IAppUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ActionScheduleDTO> Get()
        {
            var schedules = uow.ActionScheduleRepository
                                    .GetAll()
                                    .Include(a => a.Executor)
                                    .OrderBy(a => a.CreatedDate)
                                    .ToList();
            var dtos = mapper.Map<IEnumerable<ActionScheduleDTO>>(schedules);

            return dtos;
        }

        // GET api/<ScheduleController>/5
        [HttpGet("{id}")]
        public async Task<ActionScheduleDTO> Get(int id)
        {
            var actionExecutor = await uow.ActionScheduleRepository.GetById(id, a => a.Include(x => x.Executor));
            var dto = mapper.Map<ActionScheduleDTO>(actionExecutor);

            return dto;
        }

        // POST api/<ScheduleController>
        [HttpPost]
        public async Task Post([FromBody] ActionScheduleDTO dto)
        {
            var schedule = mapper.Map<ActionSchedule>(dto);
            if(schedule == null)
                throw HttpUtilsService.BadRequest("Bad ActionScheduleDTO format");

            await uow.ActionScheduleRepository.Create(schedule);
            await uow.SaveChangesAsync();
        }

        // PUT api/<ScheduleController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] ActionScheduleDTO dto)
        {
            var schedule = await uow.ActionScheduleRepository.GetById(id, a => a.Include(x => x.Executor));
            mapper.Map(dto, schedule);
            if(schedule == null)
                throw HttpUtilsService.BadRequest("Bad ActionScheduleDTO format");
            await uow.ActionScheduleRepository.Update(schedule);
            await uow.SaveChangesAsync();
        }

        // DELETE api/<ScheduleController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await uow.ActionScheduleRepository.Delete(id);
            await uow.SaveChangesAsync();
        }
    }
}
