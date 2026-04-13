using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Infrastructure.Data.Entities;

public enum KeyStatus
{
    Disabled = 0,
    Enabled = 1,
    Revoked = 2,
}

[Table("keys")]
public class KeyEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(64)]
    public string? Name { get; set; }
    
    public required Guid ServerId { get; set; }

    [ForeignKey(nameof(ServerId))]
    public ServerEntity Server { get; set; } = null!;
    
    public required Protocol Protocol { get; set; }
    
    [ForeignKey(nameof(Protocol))]
    public ProtocolConfigEntity ProtocolConfig { get; set; } = null!;
    
    public required Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserEntity User { get; set; } = null!;
    
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public KeyStatus Status { get; set; } = KeyStatus.Enabled;
}

public sealed class KeyConfiguration : IEntityTypeConfiguration<KeyEntity>
{
    public void Configure(EntityTypeBuilder<KeyEntity> entity)
    {
        entity.HasOne(e => e.ProtocolConfig).WithMany()
            .HasForeignKey(k => new { k.ServerId, k.Protocol })
            .HasPrincipalKey(p => new { p.ServerId, p.Protocol });
    }
}