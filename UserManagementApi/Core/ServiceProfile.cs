using AutoMapper;
using Core.Models;
using Data.Entities;

namespace Core
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
