using AutoMapper;
using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.Entities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Servers;

public sealed record GetServerQuery(
    Guid ServerId
) : IRequest<ServerDto?>;

public sealed class GetServerQueryHandler : IRequestHandler<GetServerQuery, ServerDto?>
{
    private readonly IMapper _mapper;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public GetServerQueryHandler(
        IMapper mapper,
        ISubscriptionRepository subscriptionRepository
    )
    {
        _mapper = mapper;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<ServerDto?> Handle(GetServerQuery request, CancellationToken cancellationToken)
    {
        var res = await _subscriptionRepository.GetByIdAsync(request.ServerId, cancellationToken);
        if (res is null) throw new ServerNotFoundException(request.ServerId);
        
        return _mapper.Map<ServerDto>(res);
    }
}