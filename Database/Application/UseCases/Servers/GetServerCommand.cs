using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.Entities;
using Database.Domain.Entities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Servers;

public sealed record GetServerQuery(
    Guid ServerId
) : IRequest<ServerDto>;

public sealed class GetServerQueryHandler : IRequestHandler<GetServerQuery, ServerDto>
{
    private readonly IServerRepository _serverRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetServerQueryHandler(
        IServerRepository serverRepository,
        IUnitOfWork unitOfWork)
    {
        _serverRepository = serverRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServerDto> Handle(GetServerQuery request, CancellationToken cancellationToken)
    {
        var server = await _serverRepository.GetByIdAsync(request.ServerId, cancellationToken);
        if (server is null) throw new ServerNotFoundException(request.ServerId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ServerDto()
        {
            Id = server.Id,
            LocationId = server.LocationId,
            IpV4Address = server.IpV4Address,
            IpV6Address = server.IpV6Address,
            DawPort = server.DawPort,
            SupportedProtocols = server.SupportedProtocols,
            SecretKey = server.SecretKey,
            IsAvailable = server.IsAvailable
        };
    }
}