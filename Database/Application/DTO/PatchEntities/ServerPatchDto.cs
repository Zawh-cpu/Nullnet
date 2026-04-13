using Database.Application.Extensions;
using Database.Domain.Entities;

namespace Database.Application.DTO.PatchEntities;

public class ServerPatchDto
{
    public OptionalField<Guid> LocationId { get; set; }
    public OptionalField<string?> IpV4Address { get; set; }
    public OptionalField<string?> IpV6Address { get; set; }
    public OptionalField<UInt16> DawPort { get; set; }
    public OptionalField<ICollection<Protocol>> SupportedProtocols { get; set; }
    public OptionalField<string> SecretKey { get; set; }
    public OptionalField<bool> IsAvailable { get; set; }
}