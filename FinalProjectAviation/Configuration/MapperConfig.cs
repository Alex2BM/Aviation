using AutoMapper;
using FinalProjectAviation.Data;
using FinalProjectAviation.DTO;

namespace FinalProjectAviation.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<User, UserPatchDTO>().ReverseMap();
            CreateMap<User, UserSignupDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

        }
    }
}
