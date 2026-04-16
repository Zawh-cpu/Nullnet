using Database.Application.UseCases.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public partial class SubscriptionController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var res = await _sender.Send(new GetSubscriptionQuery(id), cancellationToken);

        return Ok(new
        {
            Id = res!.Id,
            RevokableId = res.RevokableId,
            UserId = res.UserId,
            RateId = res.RateId,
            CreatedAt = res.CreatedAt,
            PayedUntil = res.PayedUntil
        });
    }
}

