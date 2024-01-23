using AutoMapper;
using G5.Application.Dtos;
using G5.Application.Features.Permission.Commands;

namespace Employee.Application.Mappers
{
    public class PermissionMappingProfile : Profile
    {
        public PermissionMappingProfile()
        {
            CreateMap<G5.Domain.Entities.Permission, PermissionDto>().ReverseMap();
            CreateMap<G5.Domain.Entities.Permission, RequestPermissionCommand>().ReverseMap();
            CreateMap<G5.Domain.Entities.Permission, ModifyPermissionCommand>().ReverseMap();
        }
    }
}
