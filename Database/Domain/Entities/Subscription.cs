namespace Database.Domain.Entities;

public class Subscription
{
    public Guid Id { get; set; }
    public string RevokableId { get; set; } = null!;
    
    public Guid UserId { get; set; }
    public Guid ResourceId { get; set; }
    public Guid RateId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime PayedUntil { get; set; }
}