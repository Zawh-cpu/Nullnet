using Database.Application.UseCases.Users;
using Database.Presentation.Api.v1.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public partial class UserController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var userId = await _sender.Send(new CreateUserCommand(new CreateUserCommandRequest(
            UserName: request.UserName,
            IsVerified: request.IsVerified,
            IsActive: request.IsActive
        )), cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = userId },
            new { id = userId });
    }
    
    
}