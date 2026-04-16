using Database.Application.Extensions;
using Database.Domain.Entities;

namespace Database.Presentation.Api.v1.Requests;

public class PatchServerRequest
{
    public OptionalField<Guid> LocationId { get; set; }
    public OptionalField<string?> IpV4Address { get; set; }
    public OptionalField<string?> IpV6Address { get; set; }
    public OptionalField<UInt16> DawPort { get; set; }
    public OptionalField<ICollection<Protocol>> SupportedProtocols { get; set; }
    public OptionalField<string> SecretKey { get; set; }
    public OptionalField<bool> IsAvailable { get; set; }
}