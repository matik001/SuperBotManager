using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceProvider serviceProvider;

        public ScheduleBackgroundService(ILogger<ScheduleBackgroundService> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var scope = serviceProvider.CreateScope();
                using(var uow = scope.ServiceProvider.GetRequiredService<IAppUnitOfWork>())
                {
                    var actionService = scope.ServiceProvider.GetRequiredService<IActionService>();
                    
                    var currentTime = DateTime.UtcNow;
                    var schedules = uow.ActionScheduleRepository.GetAll().Where(a => a.Enabled && a.NextRun <= currentTime).ToList();
                    foreach(var schedule in schedules)
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
}
