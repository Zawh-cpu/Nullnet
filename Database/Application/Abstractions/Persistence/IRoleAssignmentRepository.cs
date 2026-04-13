using Database.Domain.Entities;

namespace Database.Application.Abstractions.Persistence;

public interface IRoleAssignmentRepository
{
    Task AddAsync(RoleAssignment assignment, CancellationToken ct);

    Task<bool> ExistsAsync(
        Guid userId,
        Guid roleId,
        Guid resourceId,
        CancellationToken ct);
    
    Task<bool> DivestIfExistsAsync(Guid userId,
        Guid roleId,
        Guid resourceId,
        CancellationToken ct);
    
    Task<RoleAssignment?> GetAsync(Guid userId,
        Guid resourceId,
        CancellationToken ct);
}