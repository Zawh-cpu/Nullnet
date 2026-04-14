namespace Database.Application.DTO.Entities;

public class SubscriptionDto
{
    public Guid Id { get; set; }
    public string RevokableId { get; set; } = null!;
    
    public Guid UserId { get; set; }
    public Guid ResourceId { get; set; }
    public Guid RateId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime PayedUntil { get; set; }
}