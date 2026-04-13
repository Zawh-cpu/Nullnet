using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Infrastructure.Data.Entities;

[Table("users")]
public sealed class UserEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(128)]
    public string UserName { get; set; } = null!;

    [Required]
    public Guid ResourceId { get; set; }

    [ForeignKey(nameof(ResourceId))]
    public ResourceEntity Resource { get; set; } = null!;

    public ICollection<RoleAssignmentEntity> RoleAssignments { get; set; } = new List<RoleAssignmentEntity>();
    public ICollection<SubscriptionEntity> Subscriptions { get; set; } = new List<SubscriptionEntity>();
    
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> entity)
    {
        entity.HasIndex(x => x.UserName).IsUnique();

        entity.HasOne(x => x.Resource)
            .WithOne()
            .HasForeignKey<UserEntity>(x => x.ResourceId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(x => x.RoleAssignments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}