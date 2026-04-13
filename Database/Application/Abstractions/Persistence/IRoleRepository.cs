using Database.Domain.Entities;

namespace Database.Application.Abstractions.Persistence;

public interface IRoleRepository
{
    public Task AddAsync(Role user, CancellationToken ct);
    Task<Role?> GetByIdAsync(Guid id, CancellationToken ct);

    Task<Role?> GetDefaultRoleAsync(CancellationToken ct);
}