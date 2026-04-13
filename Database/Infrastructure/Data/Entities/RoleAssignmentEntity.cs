using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Infrastructure.Data.Entities;

[Table("role_assignments")]
public sealed class RoleAssignmentEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ResourceId { get; set; }

    [Required]
    public Guid RoleId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserEntity User { get; set; } = null!;

    [ForeignKey(nameof(ResourceId))]
    public ResourceEntity Resource { get; set; } = null!;

    [ForeignKey(nameof(RoleId))]
    public RoleEntity Role { get; set; } = null!;
}

public sealed class RoleAssignmentConfiguration : IEntityTypeConfiguration<RoleAssignmentEntity>
{
    public void Configure(EntityTypeBuilder<RoleAssignmentEntity> entity)
    {
        entity.HasIndex(x => x.UserId);
        entity.HasIndex(x => x.ResourceId);
        entity.HasIndex(x => x.RoleId);

        entity.HasIndex(x => new { x.UserId, x.ResourceId, x.RoleId })
            .IsUnique();
    }
}