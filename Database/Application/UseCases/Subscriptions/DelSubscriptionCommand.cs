using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.PatchEntities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Subscriptions;

public sealed record DelSubscriptionCommand(
    Guid SubscriptionId
) : IRequest;

public sealed class DelSubscriptionCommandHandler : IRequestHandler<DelSubscriptionCommand>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public async Task Handle(DelSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var processed = await _subscriptionRepository.DelByIdAsync(request.SubscriptionId, cancellationToken);

        if (processed == 0)
            throw new SubscriptionNotFoundException(request.SubscriptionId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public DelSubscriptionCommandHandler(
        ISubscriptionRepository subscriptionRepository,
        IUnitOfWork unitOfWork)
    {
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }
}