using AutoMapper;
using Database.Application.DTO.Entities;
using Database.Application.DTO.PatchEntities;
using Database.Application.UseCases.Servers;
using Database.Domain.Entities;
using Database.Infrastructure.Data.Entities;

namespace Database.Application.Profiles;

public class ServerProfile : Profile
{
    public ServerProfile()
    {
        CreateMap<ServerEntity, Server>()
            .ReverseMap();

        CreateMap<Server, ServerDto>()
            .ReverseMap();

        CreateMap<PatchServerCommandRequest, ServerPatchDto>();
    }
}