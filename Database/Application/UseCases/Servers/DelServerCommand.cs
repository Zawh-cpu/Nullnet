using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.PatchEntities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Servers;

public sealed record DelServerCommand(
    Guid ServerId
) : IRequest;

public sealed class DelServerCommandHandler : IRequestHandler<DelServerCommand>
{
    private readonly IServerRepository _serverRepository;
    private readonly IUnitOfWork _unitOfWork;

    public async Task Handle(DelServerCommand request, CancellationToken cancellationToken)
    {
        var processed = await _serverRepository.DelByIdAsync(request.ServerId, cancellationToken);

        if (processed == 0)
            throw new ServerNotFoundException(request.ServerId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public DelServerCommandHandler(
        IServerRepository serverRepository,
        IUnitOfWork unitOfWork)
    {
        _serverRepository = serverRepository;
        _unitOfWork = unitOfWork;
    }
}