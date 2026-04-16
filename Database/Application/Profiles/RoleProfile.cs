using AutoMapper;
using Database.Application.DTO.Entities;
using Database.Domain.Entities;
using Database.Infrastructure.Data.Entities;

namespace Database.Application.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<RoleEntity, Role>()
            .ReverseMap();

        CreateMap<Role, RoleDto>()
            .ReverseMap();
        
        // CreateMap<PatchRoleCommandRequest, RolePatchDto>();
        //
        // CreateMap<CreateRoleCommandRequest, Role>()
        //     .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
    }
}