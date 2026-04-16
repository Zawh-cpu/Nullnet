using Database.Application.UseCases.Servers;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public partial class ServerController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var res = await _sender.Send(new GetServerQuery(id), cancellationToken);

        return Ok(new
        {
            Id = res.Id,
            LocationId = res.LocationId,
            IpV4Address = res.IpV4Address,
            IpV6Address = res.IpV6Address,
            DawPort = res.DawPort,
            SupportedProtocols = res.SupportedProtocols,
            SecretKey = res.SecretKey,
            IsAvailable = res.IsAvailable
        });
    }
}

