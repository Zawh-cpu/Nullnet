using Database.Application.UseCases.Subscriptions;
using Database.Presentation.Api.v1.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public partial class SubscriptionController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateSubscriptionRequest request,
        CancellationToken cancellationToken)
    {
        var subscriptionId = await _sender.Send(new CreateSubscriptionCommand(new CreateSubscriptionCommandRequest(
            RevokableId: request.RevokableId,
            UserId: request.UserId,
            RateId: request.RateId,
            CreatedAt: request.CreatedAt,
            PayedUntil: request.PayedUntil
        )), cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = subscriptionId },
            new { id = subscriptionId });
    }
    
    
}