using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Infrastructure.Data.Entities;

[Table("subscriptions")]
public class SubscriptionEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(16)]
    public string RevokableId { get; set; } = null!;
    
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserEntity User { get; set; } = null!;

    [Required]
    public Guid ResourceId { get; set; }

    [ForeignKey(nameof(ResourceId))]
    public ResourceEntity Resource { get; set; } = null!;
    
    public Guid RateId { get; set; }
    [ForeignKey(nameof(RateId))]
    public RateEntity Rate { get; set; } = null!;
    
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public DateTime PayedUntil { get; set; }
}