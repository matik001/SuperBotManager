using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.Services;

namespace SuperbotSchedule
{
    [BackgroundService]
    public class ScheduleBackgroundService : BackgroundService
    {
        private readonly ILogger<ScheduleBackgroundService> logger;
        private readonly AppUnitOfWork uow;
        private readonly IActionService actionService;

        public ScheduleBackgroundService(ILogger<ScheduleBackgroundService> logger, AppUnitOfWork uow, IActionService actionService)
        {
            this.logger = logger;
            this.uow = uow;
            this.actionService = actionService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var currentTime = DateTime.UtcNow;
                var schedules = uow.ActionScheduleRepository.GetAll().Where(a => a.Enabled && a.NextRun <= currentTime).ToList();
                foreach (var schedule in schedules)
                {
                    await actionService.EnqueueExecution(schedule.ExecutorId);
                    logger.LogInformation($"Scheduled executor {schedule.ExecutorId} with schedule {schedule.ActionSCheduleName}");

                    if(schedule.Type == ActionScheduleType.Once)
                    {
                        schedule.Enabled = false;
                    }
                    else
                    {
                        schedule.NextRun = currentTime.AddSeconds(schedule.IntervalSec);
                    }
                    await uow.ActionScheduleRepository.Update(schedule);
                }

                await uow.SaveChangesAsync();
                await Task.Delay(10000, stoppingToken); /// TODO : make it configurable
            }
        }
    }
}
