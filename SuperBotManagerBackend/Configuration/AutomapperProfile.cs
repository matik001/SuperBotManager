using AutoMapper;
using SuperBotManagerBackend.DTOs;
using SuperBotManagerBase.DB.Repositories;
using Action = SuperBotManagerBase.DB.Repositories.Action;

namespace SuperBotManagerBackend.Configuration
{
    public class AutomapperProfile : Profile
    {

        public AutomapperProfile()
        {
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<ActionDefinition, ActionDefinitionDTO>().ReverseMap();

            CreateMap<ActionExecutor, ActionExecutorDTO>().ReverseMap();
            CreateMap<ActionExecutor, ActionExecutorUpdateDTO>().ReverseMap();
            CreateMap<ActionExecutor, ActionExecutorCreateDTO>().ReverseMap();
            CreateMap<ActionExecutor, ActionExecutorExtendedDTO>().ReverseMap();

            CreateMap<Action, ActionDTO>().ReverseMap();
            CreateMap<Action, ActionUpdateDTO>().ReverseMap();

            CreateMap<ActionSchedule, ActionScheduleDTO>().ReverseMap();
            CreateMap<ActionSchedule, ActionScheduleCreateDTO>().ReverseMap();
            CreateMap<ActionSchedule, ActionExecutorUpdateDTO>().ReverseMap();


            CreateMap<VaultItem, VaultItemDTO>().ReverseMap();
            CreateMap<VaultItem, VaultItemUpdateDTO>().ReverseMap();

            CreateMap<Log, LogDTO>().ReverseMap();
            CreateMap<Log, LogCreateDTO>().ReverseMap();
        }
    }
}
