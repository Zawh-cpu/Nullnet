namespace Database.Presentation.Api.v1.Requests;

public class CreateSubscriptionRequest
{
    public string RevokableId { get; set; }
    public Guid UserId { get; set; }
    public Guid ResourceId { get; set; }
    public Guid RateId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime PayedUntil { get; set; }
}