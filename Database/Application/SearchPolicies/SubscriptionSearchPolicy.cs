using Database.Application.Extensions;

namespace Database.Application.SearchPolicies;

public class SubscriptionSearchPolicy : SearchPolicy
{
    public OptionalField<Guid> Id { get; set; }
    public OptionalField<bool> IsActive { get; set; }
    public OptionalField<Guid> RateId { get; set; }
    
    public OptionalField<DateTime> CreatedAtFrom { get; set; }
    public OptionalField<DateTime> CreatedAtTo { get; set; }
    
    public OptionalField<DateTime> PayedUntilFrom { get; set; }
    public OptionalField<DateTime> PayedUntilTo { get; set; }
}
