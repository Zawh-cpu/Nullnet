using Database.Application.Abstractions.Persistence;
using Database.Domain.Entities;
using Database.Infrastructure.Data;
using Database.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Infrastructure.Repositories;

public sealed class RoleAssignmentRepository : IRoleAssignmentRepository
{
    private readonly AppDbContext _db;

    public RoleAssignmentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(RoleAssignment assignment, CancellationToken ct)
    {
        var entity = new RoleAssignmentEntity
        {
            Id = assignment.Id,
            UserId = assignment.UserId,
            RoleId = assignment.RoleId,
            ResourceId = assignment.ResourceId
        };

        await _db.RoleAssignments.AddAsync(entity, ct);
    }

    public Task<bool> ExistsAsync(
        Guid userId,
        Guid roleId,
        Guid resourceId,
        CancellationToken ct)
    {
        return _db.RoleAssignments.AnyAsync(
            x => x.UserId == userId &&
                 x.RoleId == roleId &&
                 x.ResourceId == resourceId,
            ct);
    }

    public async Task<bool> DivestIfExistsAsync(Guid userId, Guid roleId, Guid resourceId, CancellationToken ct)
    {
        return (await _db.RoleAssignments.Where(x =>
            x.UserId == userId &&
            x.RoleId == roleId &&
            x.ResourceId == resourceId)
            .ExecuteDeleteAsync(ct)) > 0;
    }

    public async Task<RoleAssignment?> GetAsync(Guid userId,
        Guid resourceId,
        CancellationToken ct)
    {
        var res = await _db.RoleAssignments
            .Where(x =>
                x.UserId == userId &&
                x.ResourceId == resourceId)
            .FirstOrDefaultAsync(ct);

        if (res is null) return null;
        
        return new RoleAssignment(res.Id, res.UserId, res.ResourceId, res.RoleId);
    }
}