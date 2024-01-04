using AutoMapper;
using SuperBotManagerBackend.DB.Repositories;
using SuperBotManagerBackend.DTOs;

namespace SuperBotManagerBackend.Configuration
{
    public class AutomapperProfile : Profile
    {

        public AutomapperProfile()
        {
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
