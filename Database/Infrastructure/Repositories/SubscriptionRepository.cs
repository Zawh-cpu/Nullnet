using AutoMapper;
using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.PatchEntities;
using Database.Domain.Entities;
using Database.Infrastructure.Data;
using Database.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public SubscriptionRepository(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task AddAsync(Subscription subscription, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<SubscriptionEntity>(subscription);
        await _db.Subscriptions.AddAsync(data, cancellationToken);
    }

    public async Task<int> DelByIdAsync(Guid subscriptionId, CancellationToken cancellationToken)
    {
        return await _db.Servers.Where(x => x.Id == subscriptionId).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<Subscription?> GetByIdAsync(Guid subscriptionId, CancellationToken cancellationToken)
    {
        var data = await _db.Subscriptions.FirstOrDefaultAsync(x => x.Id == subscriptionId, cancellationToken);
        if (data == null) return null;
        
        return _mapper.Map<Subscription>(data);
    }

    public async Task<int> UpdateByIdAsync(Guid subscriptionId, SubscriptionPatchDto patch, CancellationToken cancellationToken)
    {
        var query = _db.Subscriptions.Where(x => x.Id == subscriptionId);

        return await query.ExecuteUpdateAsync(s =>
        {
            if (patch.RevokableId.HasValue)
                s.SetProperty(x => x.RevokableId, patch.RevokableId.Value);
            
            if (patch.UserId.HasValue)
                s.SetProperty(x => x.UserId, patch.UserId.Value);
            
            if (patch.ResourceId.HasValue)
                s.SetProperty(x => x.ResourceId, patch.ResourceId.Value);
            
            if (patch.RateId.HasValue)
                s.SetProperty(x => x.RateId, patch.RateId.Value);
            
            if (patch.PayedUntil.HasValue)
                s.SetProperty(x => x.PayedUntil, patch.PayedUntil.Value);
        }, cancellationToken);
    }
}