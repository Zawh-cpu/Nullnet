namespace Database.Domain.Entities;

public class Server
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid LocationId { get; set; }
    public string? IpV4Address { get; set; }
    public string? IpV6Address { get; set; }
    public UInt16 DawPort { get; set; }
    public ICollection<Protocol> SupportedProtocols { get; set; } = new List<Protocol>();
    public string SecretKey { get; set; } = null!;
    public bool IsAvailable { get; set; } = true;

    public static Server Create(Guid locationId, string? ipV4Address,
                         string? ipV6Address, UInt16 dawPort,
                         ICollection<Protocol> supportedProtocols,
                         string secretKey, bool isActive = true)
    {
        return new Server()
        {
            Id = Guid.NewGuid(),
            LocationId = locationId,
            IpV4Address = ipV4Address,
            IpV6Address = ipV6Address,
            DawPort = dawPort,
            SupportedProtocols = supportedProtocols,
            SecretKey = secretKey,
            IsAvailable = isActive
        };
    }
}