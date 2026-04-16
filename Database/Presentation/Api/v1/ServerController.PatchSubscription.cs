using Database.Application.UseCases.Servers;
using Database.Presentation.Api.v1.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public partial class ServerController
{
    [HttpPost("{id:guid}")]
    public async Task<IActionResult> PatchServer(
        [FromQuery] Guid id,
        [FromBody] PatchServerRequest request,
        CancellationToken cancellationToken)
    {
        var serverId = await _sender.Send(new PatchServerCommand(id, new PatchServerCommandRequest(
            LocationId: request.LocationId,
            IpV4Address: request.IpV4Address,
            IpV6Address: request.IpV6Address,
            DawPort: request.DawPort,
            SupportedProtocols: request.SupportedProtocols,
            SecretKey: request.SecretKey,
            IsAvailable: request.IsAvailable
        )), cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = serverId },
            new { id = serverId });
    }
    
    
}