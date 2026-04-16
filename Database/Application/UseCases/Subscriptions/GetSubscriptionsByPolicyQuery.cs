using AutoMapper;
using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.Entities;
using Database.Application.SearchPolicies;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Subscriptions;


public sealed record GetSubscriptionsByPolicyQuery(
    SubscriptionSearchPolicy Policy
) : IRequest<ICollection<SubscriptionDto>>;

public sealed class GetSubscriptionsQueryHandler : IRequestHandler<GetSubscriptionsByPolicyQuery, ICollection<SubscriptionDto>>
{
    private readonly IMapper _mapper;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public GetSubscriptionsQueryHandler(
        IMapper mapper,
        ISubscriptionRepository subscriptionRepository
    )
    {
        _mapper = mapper;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<ICollection<SubscriptionDto>> Handle(GetSubscriptionsByPolicyQuery request, CancellationToken cancellationToken)
    {
        var res = await _subscriptionRepository.GetByPolicyAsync(request.Policy, cancellationToken);
        
        return res.Select(x => _mapper.Map<SubscriptionDto>(x)).ToList();
    }
}