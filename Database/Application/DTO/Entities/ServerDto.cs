using Database.Domain.Entities;

namespace Database.Application.DTO.Entities;

public class ServerDto
{
    public Guid Id { get; set; }
        
    public Guid LocationId { get; set; }
    
    public string? IpV4Address { get; set; }
        
    public string? IpV6Address { get; set; }
        
    public UInt16 DawPort { get; set; }

    public ICollection<Protocol> SupportedProtocols { get; set; } = new List<Protocol>();
    
    public string SecretKey { get; set; } = null!;
    public bool IsAvailable { get; set; } = true;
}