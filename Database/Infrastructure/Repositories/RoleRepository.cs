using Database.Application.Abstractions.Persistence;
using Database.Domain.Entities;
using Database.Infrastructure.Data;
using Database.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Infrastructure.Repositories;

public sealed class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _db;

    public RoleRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task AddAsync(Role role, CancellationToken ct)
    {
        var entity = new RoleEntity()
        {
            Id = role.Id,
            Code = role.Code,
            Name = role.Name,
            IsDefault = role.IsDefault,
        };

        await _db.Roles.AddAsync(entity, ct);
    }

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var entity = await _db.Roles
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (entity is null) return null;

        return new Role(entity.Id, entity.Code, entity.Name, entity.IsDefault);
    }

    public async Task<Role?> GetDefaultRoleAsync(CancellationToken ct)
    {
        var entity = await _db.Roles
            .FirstOrDefaultAsync(x => x.IsDefault, ct);

        if (entity is null) return null;

        return new Role(entity.Id, entity.Code, entity.Name, entity.IsDefault);
    }
}