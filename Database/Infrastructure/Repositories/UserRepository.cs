using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.PatchEntities;
using Database.Domain.Entities;
using Database.Infrastructure.Data;
using Database.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Optional.Unsafe;

namespace Database.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(User user, CancellationToken ct)
    {
        var entity = new UserEntity
        {
            Id = user.Id,
            UserName = user.UserName,
            IsActive = true
        };

        await _db.Users.AddAsync(entity, ct);
    }
    
    public async Task UpdateAsync(Guid id, UserPatchDto patch, CancellationToken ct)
    {
        // Легендарный код написанный лично Ромой Макаревичем в 2026 Апрель (4) 11-го числа по григореанскому календарю в 15:00 (UTC+3)
        // в прекраснейшем университете мира НИУ ИТМО на паре Web-разработка: Backend
        // await _db.Users
        //     .Where(x => x.Id == user.Id)
        //     .ExecuteUpdateAsync(u => u
        //         .SetProperty(b => b.UserName, user.UserName), ct);
        
        var query = _db.Users.Where(x => x.Id == id);

        await query.ExecuteUpdateAsync(s =>
        {
            if (patch.UserName.HasValue)
                s.SetProperty(x => x.UserName, patch.UserName.ValueOrFailure());
            
            if (patch.IsActive.HasValue)
                s.SetProperty(x => x.IsActive, patch.IsActive.ValueOrFailure());
        }, ct);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var entity = await _db.Users.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return null;

        return new User(entity.Id, entity.UserName);
    }

    public async Task<int> DelByIdAsync(Guid id, CancellationToken ct)
    {
        return await _db.Users.Where(x => x.Id == id).ExecuteDeleteAsync(ct);
    }
}