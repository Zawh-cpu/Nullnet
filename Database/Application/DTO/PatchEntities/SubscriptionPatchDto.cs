using Database.Application.Extensions;

namespace Database.Application.DTO.PatchEntities;

public class SubscriptionPatchDto
{
    public OptionalField<string> RevokableId { get; set; } = null!;
    
    public OptionalField<Guid> UserId { get; set; }
    public OptionalField<Guid> ResourceId { get; set; }
    public OptionalField<Guid> RateId { get; set; }
    
    public OptionalField<DateTime> PayedUntil { get; set; }
}