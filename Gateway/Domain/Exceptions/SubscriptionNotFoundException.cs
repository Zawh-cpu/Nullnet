namespace Gateway.Domain.Exceptions;

public sealed class SubscriptionNotFoundException : AppException
{
    public SubscriptionNotFoundException(Guid subscriptionId)
        : base(
            message: $"Subscription with id '{subscriptionId}' was not found.",
            statusCode: StatusCodes.Status404NotFound,
            title: "Subscription not found",
            type: "https://httpstatuses.com/404")
    {
    }
}