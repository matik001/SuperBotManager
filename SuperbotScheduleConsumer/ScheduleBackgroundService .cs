using Microsoft.Extensions.Configuration;
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
        private readonly IConfigurationManager configurationManager;

        int intervalMs;
        public ScheduleBackgroundService(ILogger<ScheduleBackgroundService> logger, IServiceProvider serviceProvider, IConfigurationManager configurationManager)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.configurationManager = configurationManager;
            intervalMs = configurationManager?.GetSection("Scheduler")?.GetValue<int>("IntervalMs") ?? 10000;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                using var scope = serviceProvider.CreateScope();
                using var uow = scope.ServiceProvider.GetRequiredService<IAppUnitOfWork>();

                var currentTime = DateTime.UtcNow;
                var schedules = uow.ActionScheduleRepository.GetAll().Where(a => a.Enabled && a.NextRun <= currentTime).ToList();
                

                IActionService actionService = null;
                if(schedules.Count >= 1) /// do not connect to rabbitmq if not necessary
                    actionService = scope.ServiceProvider.GetRequiredService<IActionService>();

                foreach(var schedule in schedules)
                {
                    await actionService.EnqueueExecution(schedule.ExecutorId, RunStartType.Scheduled);
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
                if(schedules.Count >= 1)
                    await uow.SaveChangesAsync();

                await Task.Delay(intervalMs, stoppingToken);
                
            }
        }
    }
}
