using AutoMapper;
using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.PatchEntities;
using Database.Application.Extensions;
using Database.Domain.Entities;
using Database.Domain.Exceptions;
using MediatR;

namespace Database.Application.UseCases.Servers;

public sealed record PatchServerCommand(
    Guid ServerId,
    
    OptionalField<Guid> LocationId,
    OptionalField<string?> IpV4Address,
    OptionalField<string?> IpV6Address,
    OptionalField<UInt16> DawPort,
    OptionalField<ICollection<Protocol>> SupportedProtocols,
    OptionalField<string> SecretKey,
    OptionalField<bool> IsAvailable
) : IRequest<Guid>;

public sealed class PatchServerCommandHandler : IRequestHandler<PatchServerCommand, Guid>
{
    private readonly IServerRepository _serverRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatchServerCommandHandler(
        IServerRepository serverRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _serverRepository = serverRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(PatchServerCommand request, CancellationToken cancellationToken)
    {
        var data = await _serverRepository.GetByIdAsync(request.ServerId, cancellationToken);
        if (data == null) throw new ServerNotFoundException(request.ServerId);
        
        await _serverRepository.PatchByIdAsync(data.Id,
            _mapper.Map<ServerPatchDto>(request),
            cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return data.Id;
    }
}