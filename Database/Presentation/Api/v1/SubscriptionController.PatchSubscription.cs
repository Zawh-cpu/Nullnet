using Database.Application.UseCases.Subscriptions;
using Database.Presentation.Api.v1.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public partial class SubscriptionController
{
    [HttpPost("{id:guid}")]
    public async Task<IActionResult> PatchSubscription(
        [FromQuery] Guid id,
        [FromBody] PatchSubscriptionRequest request,
        CancellationToken cancellationToken)
    {
        var subscriptionId = await _sender.Send(new PatchSubscriptionCommand(id, new PatchSubscriptionCommandRequest(
            RevokableId: request.RevokableId,
            UserId: request.UserId,
            RateId: request.RateId,
            PayedUntil: request.PayedUntil,
            IsActive: request.IsActive
        )), cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = subscriptionId },
            new { id = subscriptionId });
    }
    
    
}