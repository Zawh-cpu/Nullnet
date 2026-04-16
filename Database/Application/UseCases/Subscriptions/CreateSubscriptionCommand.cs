using AutoMapper;
using Database.Application.Abstractions.Persistence;
using Database.Domain.Entities;
using MediatR;

namespace Database.Application.UseCases.Subscriptions;

public sealed record CreateSubscriptionCommandRequest(
    string RevokableId,
    Guid UserId,
    Guid RateId,
    DateTime CreatedAt,
    DateTime PayedUntil
);

public sealed record CreateSubscriptionCommand(
    CreateSubscriptionCommandRequest Data
) : IRequest<Guid>;

public sealed class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(
        IMapper mapper,
        ISubscriptionRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _subscriptionRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = _mapper.Map<Subscription>(request.Data);

        await _subscriptionRepository.AddAsync(subscription, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return subscription.Id;
    }
}