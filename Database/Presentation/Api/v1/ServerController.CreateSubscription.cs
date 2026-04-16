using Database.Application.UseCases.Servers;
using Database.Presentation.Api.v1.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public partial class ServerController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateServerRequest request,
        CancellationToken cancellationToken)
    {
        var serverId = await _sender.Send(new CreateServerCommand(new CreateServerCommandRequest(
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