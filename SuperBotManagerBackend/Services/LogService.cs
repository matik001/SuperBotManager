using AutoMapper;
using SuperBotManagerBackend.DTOs;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.DB.Repositories;

namespace SuperBotManagerBackend.Services
{
    public interface ILogService<T> where T : class
    {
        
    }

    public class LogService<T> : ILogService<T> where T : class
    {
        private readonly IAppUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public string ModuleName { get; private set; }
        public string LogApp { get; private set; }

        public LogService(IAppUnitOfWork uow, IMapper mapper, IAuthService authService , string logApp = "SuperBot")
        {
            this.uow = uow;
            this.mapper = mapper;
            this.authService = authService;
            this.ModuleName = typeof(T).Name;
            this.LogApp = logApp;
        }

        public async Task Log(string title, string details, LogType type)
        {
            var user = await authService.GetCurrentUser();
            await uow.LogRepository.Create(new Log
            {
                LogApp = LogApp,
                LogDetails = details,
                LogTitle = title,
                LogModule = ModuleName,
                LogType = type,
                UserId = user?.Id
            });
            await uow.SaveChangesAsync();
        }
        public async Task LogInfo(string title, string details)
        {
            await Log(title, details, LogType.Info);
        }
        public async Task LogError(string title, string details)
        {
            await Log(title, details, LogType.Info);
        }
    }
}
