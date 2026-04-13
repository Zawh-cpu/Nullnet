using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Infrastructure.Data.Entities;

[Table("resources")]
public sealed class ResourceEntity
{
    [Key]
    public Guid Id { get; set; }

    public Guid? ParentId { get; set; }

    [Required]
    public ResourceType Type { get; set; }

    [Required]
    [MaxLength(256)]
    public string Name { get; set; } = null!;

    // Владелец ресурса (для SELF / OWNED)
    public Guid? OwnerUserId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public ResourceEntity? Parent { get; set; }

    public ICollection<ResourceEntity> Children { get; set; } = new List<ResourceEntity>();

    public ICollection<RoleAssignmentEntity> RoleAssignments { get; set; } = new List<RoleAssignmentEntity>();
}

public sealed class ResourceConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> entity)
    {
        entity.HasIndex(x => x.ParentId);
        entity.HasIndex(x => x.Type);
        entity.HasIndex(x => x.OwnerUserId);

        entity.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(x => x.RoleAssignments)
            .WithOne(x => x.Resource)
            .HasForeignKey(x => x.ResourceId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(x => x.OwnerUserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}