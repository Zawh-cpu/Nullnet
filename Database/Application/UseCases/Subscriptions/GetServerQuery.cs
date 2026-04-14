using AutoMapper;
using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.Entities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Subscriptions;

public sealed record GetSubscriptionQuery(
    Guid SubscriptionId
) : IRequest<SubscriptionDto?>;

public sealed class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, SubscriptionDto?>
{
    private readonly IMapper _mapper;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public GetSubscriptionQueryHandler(
        IMapper mapper,
        ISubscriptionRepository subscriptionRepository
    )
    {
        _mapper = mapper;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<SubscriptionDto?> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
    {
        var res = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId, cancellationToken);
        if (res is null) throw new SubscriptionNotFoundException(request.SubscriptionId);
        
        return _mapper.Map<SubscriptionDto>(res);
    }
}