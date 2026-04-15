using AutoMapper;
using Database.Application.DTO.Entities;
using Database.Application.DTO.PatchEntities;
using Database.Application.UseCases.Users;
using Database.Domain.Entities;
using Database.Infrastructure.Data.Entities;

namespace Database.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, User>()
            .ReverseMap();

        CreateMap<User, UserDto>()
            .ReverseMap();
        
        CreateMap<PatchUserCommandRequest, UserPatchDto>();
        
        CreateMap<CreateUserCommandRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
    }
}