using Database.Application.Extensions;

namespace Database.Presentation.Api.v1.Requests;

public class PatchSubscriptionRequest
{
    public OptionalField<string> RevokableId { get; set; }
    public OptionalField<Guid> UserId { get; set; }
    public OptionalField<Guid> RateId { get; set; }
    
    public OptionalField<DateTime> PayedUntil { get; set; }
    public OptionalField<bool> IsActive { get; set; }
}