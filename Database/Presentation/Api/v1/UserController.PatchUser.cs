using Database.Application.UseCases.Users;
using Database.Presentation.Api.v1.Requests;
using Microsoft.AspNetCore.Mvc;
using PatchUserRequest = Database.Presentation.Api.v1.Requests.PatchUserRequest;

namespace Database.Presentation.Api.v1;

public partial class UserController
{
    [HttpPatch("{userId:guid}")]
    public async Task<IActionResult> Patch(
        [FromQuery] Guid userId,
        [FromBody] PatchUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new PatchUserCommand(
            userId,
            new Application.UseCases.Users.PatchUserRequest(
                    request.UserName,
                    request.IsActive,
                )
        );

        return CreatedAtAction(
            nameof(GetById),
            new { id = userId },
            new { id = userId });
    }
}