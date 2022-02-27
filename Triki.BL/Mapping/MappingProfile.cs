using AutoMapper;
using Triki.CI.Dto;
using Triki.CI.Models;

namespace Triki.BL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
