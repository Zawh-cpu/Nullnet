using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Database.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Infrastructure.Data.Entities;

[Table("protocol_configs")]
public class ProtocolConfigEntity
{
    public required Protocol Protocol { get; set; }
    public required Guid ServerId { get; set; }
    
    [ForeignKey(nameof(ServerId))]
    public ServerEntity Server { get; set; } = null!;
    
    public required uint Port { get; set; }
    
    public required JsonDocument ExtraConfiguration { get; set; }
    public required JsonDocument ExtraPublicConfiguration { get; set; }
}

public sealed class ProtocolConfigConfiguration : IEntityTypeConfiguration<ProtocolConfigEntity>
{
    public void Configure(EntityTypeBuilder<ProtocolConfigEntity> entity)
    {
        entity.HasKey(x => new { x.ServerId, x.Protocol });
    }
}