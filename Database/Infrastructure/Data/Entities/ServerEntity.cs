using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Domain.Entities;

namespace Database.Infrastructure.Data.Entities;

[Table("servers")]
public class ServerEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
        
    public Guid LocationId { get; set; }
        
    [ForeignKey(nameof(LocationId))]
    public LocationEntity? Location { get; set; }
        
    [StringLength(21)]
    public string? IpV4Address { get; set; }
        
    [StringLength(49)]
    public string? IpV6Address { get; set; }
        
    [Required]
    public UInt16 DawPort { get; set; }

    public ICollection<Protocol> SupportedProtocols { get; set; } = new List<Protocol>();
    
    [Required]
    [MaxLength(64)]
    public string SecretKey { get; set; } = null!;
    public bool IsAvailable { get; set; } = true;
}