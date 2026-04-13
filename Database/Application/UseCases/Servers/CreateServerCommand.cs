using Database.Application.Abstractions.Persistence;
using Database.Domain.Entities;
using MediatR;

namespace Database.Application.UseCases.Servers;

public sealed record CreateServerCommand(
    Guid LocationId, string? IpV4Address,
    string? IpV6Address, UInt16 DawPort,
    ICollection<Protocol> SupportedProtocols,
    string SecretKey, bool IsActive = true
) : IRequest<Guid>;

public sealed class CreateServerCommandHandler : IRequestHandler<CreateServerCommand, Guid>
{
    private readonly IServerRepository _serverRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateServerCommandHandler(
        IServerRepository serverRepository,
        IUnitOfWork unitOfWork)
    {
        _serverRepository = serverRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateServerCommand request, CancellationToken cancellationToken)
    {
        var data = Server.Create(request.LocationId, request.IpV4Address, request.IpV6Address, request.DawPort,
            request.SupportedProtocols, request.SecretKey, request.IsActive);

        await _serverRepository.AddAsync(data, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return data.Id;
    }
}