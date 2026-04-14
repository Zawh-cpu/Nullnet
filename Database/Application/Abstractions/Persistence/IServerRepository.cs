using Database.Application.DTO.PatchEntities;
using Database.Domain.Entities;

namespace Database.Application.Abstractions.Persistence;

public interface IServerRepository
{
    public Task AddAsync(Server user, CancellationToken ct);
    Task<Server?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<int> DelByIdAsync(Guid id, CancellationToken ct);
    Task UpdateByIdAsync(Guid serverId, ServerPatchDto map, CancellationToken cancellationToken);
}