using AutoMapper;
using Database.Application.Abstractions.Persistence;
using Database.Application.DTO.PatchEntities;
using Database.Application.SearchPolicies;
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

    public async Task<ICollection<Subscription>> GetByPolicyAsync(
        SubscriptionSearchPolicy policy,
        CancellationToken cancellationToken)
    {
        IQueryable<SubscriptionEntity> query = _db.Subscriptions.AsNoTracking();

        if (policy.Id.HasValue)
            query = query.Where(x => x.Id == policy.Id.Value);

        if (policy.IsActive.HasValue)
            query = query.Where(x => x.IsActive == policy.IsActive.Value);

        if (policy.RateId.HasValue)
            query = query.Where(x => x.RateId == policy.RateId.Value);

        if (policy.CreatedAtFrom.HasValue)
            query = query.Where(x => x.CreatedAt >= policy.CreatedAtFrom.Value);

        if (policy.CreatedAtTo.HasValue)
            query = query.Where(x => x.CreatedAt <= policy.CreatedAtTo.Value);

        if (policy.PayedUntilFrom.HasValue)
            query = query.Where(x => x.PayedUntil >= policy.PayedUntilFrom.Value);

        if (policy.PayedUntilTo.HasValue)
            query = query.Where(x => x.PayedUntil <= policy.PayedUntilTo.Value);

        query = query
            .Skip((int)policy.Pagination.Offset)
            .Take((int)policy.Pagination.Limit);

        return await query.Select(x => _mapper.Map<Subscription>(x)).ToListAsync(cancellationToken);
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