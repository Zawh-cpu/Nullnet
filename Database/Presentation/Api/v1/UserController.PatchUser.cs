using Database.Application.UseCases.Users;
using Database.Presentation.Api.v1.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public partial class UserController
{
    [HttpPatch("{userId:guid}")]
    public async Task<IActionResult> Patch(
        [FromQuery] Guid userId,
        [FromBody] PatchUserRequest request,
        CancellationToken cancellationToken)
    {
        var res = _sender.Send(new PatchUserCommand(
            userId,
            new PatchUserCommandRequest(
                UserName: request.UserName,
                IsVerified: request.IsVerified,
                IsActive: request.IsActive
            )
        ), cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = res.Result },
            new { id = res.Result });
    }
}