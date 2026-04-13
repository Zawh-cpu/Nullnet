using Database.Application.DTO.PatchEntities;
using Database.Domain.Entities;

namespace Database.Application.Abstractions.Persistence;

public interface IUserRepository
{
    public Task AddAsync(User user, CancellationToken ct);
    public Task UpdateAsync(Guid id, UserPatchDto patch, CancellationToken ct);

    public Task<User?> GetByIdAsync(Guid id, CancellationToken ct);
    public Task<int> DelByIdAsync(Guid id, CancellationToken ct);
}