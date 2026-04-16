using AutoMapper;
using Database.Application.Extensions;
using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.PatchEntities;
using Database.Domain.Entities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Subscriptions;

public sealed record PatchSubscriptionCommandRequest(
    OptionalField<string> RevokableId,
    OptionalField<Guid> UserId,
    OptionalField<Guid> RateId,
    OptionalField<DateTime> PayedUntil,
    OptionalField<bool> IsActive
);

public sealed record PatchSubscriptionCommand(
    Guid SubscriptionId,
    PatchSubscriptionCommandRequest Patch
) : IRequest<Guid>;

public sealed class PatchSubscriptionCommandHandler : IRequestHandler<PatchSubscriptionCommand, Guid>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatchSubscriptionCommandHandler(
        ISubscriptionRepository subscriptionRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Guid> Handle(PatchSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var proccessed = await _subscriptionRepository.UpdateByIdAsync(
            request.SubscriptionId,
            _mapper.Map<SubscriptionPatchDto>(request.Patch),
            cancellationToken);
        
        if (proccessed == 0)
            throw new SubscriptionNotFoundException(request.SubscriptionId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return request.SubscriptionId;
    }
}