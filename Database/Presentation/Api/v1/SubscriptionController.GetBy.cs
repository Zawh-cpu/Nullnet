using Database.Application.Extensions;
using Database.Application.SearchPolicies;
using Database.Application.UseCases.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public class GetSubscriptionByRequest
{
    public UInt64 Limit { get; set; } = 50;
    public UInt64 Offset { get; set; } = 0;
    
    public Guid? Id { get; set; }
    public bool? IsActive { get; set; }
    public Guid? RateId { get; set; }
    
    public DateTime? CreatedAtFrom { get; set; }
    public DateTime? CreatedAtTo { get; set; }
    
    public DateTime? PayedUntilFrom { get; set; }
    public DateTime? PayedUntilTo { get; set; }
}

public partial class SubscriptionController
{
    [HttpGet]
    public async Task<IActionResult> GetBy([FromQuery] GetSubscriptionByRequest query, CancellationToken ct)
    {
        var policy = new SubscriptionSearchPolicy
        {
            Pagination = new(query.Limit, query.Offset),
            Id = query.Id.HasValue ? OptionalField<Guid>.Some(query.Id.Value) : OptionalField<Guid>.None(),
            IsActive = query.IsActive.HasValue ? OptionalField<bool>.Some(query.IsActive.Value) : OptionalField<bool>.None(),
            RateId = query.RateId.HasValue ? OptionalField<Guid>.Some(query.RateId.Value) : OptionalField<Guid>.None(),
            CreatedAtFrom = query.CreatedAtFrom.HasValue ? OptionalField<DateTime>.Some(query.CreatedAtFrom.Value) : OptionalField<DateTime>.None(),
            CreatedAtTo = query.CreatedAtTo.HasValue ? OptionalField<DateTime>.Some(query.CreatedAtTo.Value) : OptionalField<DateTime>.None(),
            PayedUntilFrom = query.PayedUntilFrom.HasValue ? OptionalField<DateTime>.Some(query.PayedUntilFrom.Value) : OptionalField<DateTime>.None(),
            PayedUntilTo = query.PayedUntilTo.HasValue ? OptionalField<DateTime>.Some(query.PayedUntilTo.Value) : OptionalField<DateTime>.None(),
        };

        var subscriptions = await _sender.Send(new GetSubscriptionsByPolicyQuery(policy), ct);

        return Ok(subscriptions.Select(res => new
        {
            Id = res.Id,
            RevokableId = res.RevokableId,
            UserId = res.UserId,
            ResourceId = res.ResourceId,
            RateId = res.RateId,
            CreatedAt = res.CreatedAt,
            PayedUntil = res.PayedUntil
        }));
    }
}