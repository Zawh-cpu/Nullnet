using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Infrastructure.Data.Entities;

[Table("roles")]
public sealed class RoleEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Code { get; set; } = null!;

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = null!;

    public ICollection<RolePermissionEntity> Permissions { get; set; } = new List<RolePermissionEntity>();

    public ICollection<RoleAssignmentEntity> Assignments { get; set; } = new List<RoleAssignmentEntity>();
    
    public bool IsDefault { get; set; }
}

public sealed class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> entity)
    {
        entity.HasIndex(x => x.Id)
            .IsUnique()
            .HasFilter("\"IsDefault\" = TRUE");;

        entity.HasMany(x => x.Permissions)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasMany(x => x.Assignments)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}