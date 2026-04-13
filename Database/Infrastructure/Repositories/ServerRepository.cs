using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.PatchEntities;
using Database.Domain.Entities;
using Database.Infrastructure.Data;
using Database.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Optional.Unsafe;

namespace Database.Infrastructure.Repositories;

public class ServerRepository : IServerRepository
{
    private readonly AppDbContext _db;

    public ServerRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public async Task AddAsync(Server server, CancellationToken ct)
    {
        var entity = new ServerEntity
        {
            Id = server.Id,
            LocationId = server.LocationId,
            IpV4Address = server.IpV4Address,
            IpV6Address = server.IpV6Address,
            DawPort = server.DawPort,
            SupportedProtocols = server.SupportedProtocols,
            SecretKey = server.SecretKey,
            IsAvailable = server.IsAvailable,
        };

        await _db.Servers.AddAsync(entity, ct);
    }

    public async Task<Server?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var entity = await _db.Servers.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return null;

        return new Server()
        {
            Id = entity.Id,
            LocationId = entity.LocationId,
            IpV4Address = entity.IpV4Address,
            IpV6Address = entity.IpV6Address,
            DawPort = entity.DawPort,
            SupportedProtocols = entity.SupportedProtocols,
            SecretKey = entity.SecretKey,
            IsAvailable = entity.IsAvailable,
        };
    }

    public async Task PatchByIdAsync(Guid id, ServerPatchDto patch, CancellationToken ct)
    {
        var query = _db.Servers.Where(x => x.Id == id);

        await query.ExecuteUpdateAsync(s =>
        {
            if (patch.LocationId.HasValue)
                s.SetProperty(x => x.LocationId, patch.LocationId.ValueOrFailure());

            if (patch.IpV4Address.HasValue)
                s.SetProperty(x => x.IpV4Address, patch.IpV4Address.ValueOrFailure());

            if (patch.IpV6Address.HasValue)
                s.SetProperty(x => x.IpV6Address, patch.IpV6Address.ValueOrFailure());

            if (patch.SecretKey.HasValue)
                s.SetProperty(x => x.SecretKey, patch.SecretKey.ValueOrFailure());

            if (patch.IsAvailable.HasValue)
                s.SetProperty(x => x.IsAvailable, patch.IsAvailable.ValueOrFailure());
        }, ct);
    }
}