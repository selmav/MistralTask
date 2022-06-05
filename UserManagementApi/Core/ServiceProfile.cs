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
                .ForMember(dest => dest.UserPermissions, opt => opt.Ignore())
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Status.StatusId))
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Username, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<Permission, PermissionDto>().ReverseMap();

            CreateMap<Status, StatusDto>()
                .ReverseMap()
                .ForMember(dest => dest.Name, opt => opt.Ignore());
        }
    }
}
