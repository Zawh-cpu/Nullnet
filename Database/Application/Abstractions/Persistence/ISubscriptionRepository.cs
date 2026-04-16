using Database.Application.DTO.PatchEntities;
using Database.Application.SearchPolicies;
using Database.Domain.Entities;

namespace Database.Application.Abstractions.Persistence;

public interface ISubscriptionRepository
{
    Task AddAsync(Subscription subscription, CancellationToken cancellationToken);
    Task<int> DelByIdAsync(Guid subscriptionId, CancellationToken cancellationToken);
    Task<Subscription?> GetByIdAsync(Guid subscriptionId, CancellationToken cancellationToken);
    Task<ICollection<Subscription>> GetByPolicyAsync(SubscriptionSearchPolicy policy, CancellationToken cancellationToken);
    Task<int> UpdateByIdAsync(Guid subscriptionId, SubscriptionPatchDto patchDto, CancellationToken cancellationToken);
}