using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Infrastructure.Data.Entities;

[Table("role_permissions")]
public sealed class RolePermissionEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid RoleId { get; set; }

    [Required]
    public Guid PermissionId { get; set; }

    [Required]
    public PermissionScope Scope { get; set; }

    [ForeignKey(nameof(RoleId))]
    public RoleEntity Role { get; set; } = null!;

    [ForeignKey(nameof(PermissionId))]
    public PermissionEntity Permission { get; set; } = null!;
}

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
    public void Configure(EntityTypeBuilder<RolePermissionEntity> entity)
    {
        entity.HasKey(x => new { x.RoleId, x.PermissionId });

        entity.Property(x => x.Scope)
            .HasConversion<int>()
            .IsRequired();
    }
}