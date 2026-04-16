using Database.Application.UseCases.Roles;
using Database.Presentation.Api.v1.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public partial class RoleController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateRoleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateRoleCommand(
            request.Code,
            request.Name,
            request.IsDefault
        );

        var roleId = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = roleId },
            new { id = roleId });
    }
}