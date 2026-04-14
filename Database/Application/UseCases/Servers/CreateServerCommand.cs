using AutoMapper;
using Database.Application.Abstractions.Persistence;
using Database.Domain.Entities;
using MediatR;

namespace Database.Application.UseCases.Servers;

public sealed record CreateServerCommandRequest(
    Guid LocationId,
    string? IpV4Address,
    string? IpV6Address,
    UInt16 DawPort,
    ICollection<Protocol> SupportedProtocols,
    string SecretKey,
    bool IsAvailable
);

public sealed record CreateServerCommand(
    CreateServerCommandRequest Data
) : IRequest<Guid>;

public sealed class CreateServerCommandHandler : IRequestHandler<CreateServerCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IServerRepository _serverRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateServerCommandHandler(
        IMapper mapper,
        IServerRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _serverRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateServerCommand request, CancellationToken cancellationToken)
    {
        var server = _mapper.Map<Server>(request.Data);

        await _serverRepository.AddAsync(server, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return server.Id;
    }
}