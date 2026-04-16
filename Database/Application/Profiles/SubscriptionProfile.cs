using AutoMapper;
using Database.Application.DTO.Entities;
using Database.Application.DTO.PatchEntities;
using Database.Application.UseCases.Subscriptions;
using Database.Domain.Entities;
using Database.Infrastructure.Data.Entities;

namespace Database.Application.Profiles;

public class SubscriptionProfile : Profile
{
    public SubscriptionProfile()
    {
        CreateMap<SubscriptionEntity, Subscription>()
            .ReverseMap();

        CreateMap<Subscription, SubscriptionDto>()
            .ReverseMap();
        
        CreateMap<PatchSubscriptionCommandRequest, SubscriptionPatchDto>();
        
        CreateMap<CreateSubscriptionCommandRequest, Subscription>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
    }
}