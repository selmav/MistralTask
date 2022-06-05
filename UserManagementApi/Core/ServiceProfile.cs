using AutoMapper;
using Core.Models;
using Data.Entities;
using System.Linq;

namespace Core
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.PermissionIds, opt => opt.MapFrom(src => src.UserPermissions.Select(p => p.PermissionId)))
                .ReverseMap()
                .ForMember(dest => dest.UserPermissions, opt => opt.Ignore());

            CreateMap<Permission, PermissionDto>().ReverseMap();
        }
    }
}
