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
        var command = new CreateUserCommand(
            request.UserName
        );

        var userId = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = userId },
            new { id = userId });
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(new { id });
    }
}