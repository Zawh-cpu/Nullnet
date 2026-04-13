using AutoMapper;
using Database.Application.DTO.Entities;
using Database.Domain.Entities;

namespace Database.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}