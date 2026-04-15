using Database.Application.UseCases.Users;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public partial class UserController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var res = await _sender.Send(new GetUserQuery(id), cancellationToken);
        return Ok(new
        {
            Id = res.Id,
            UserName = res.UserName,
            ResourceId = res.ResourceId,
            IsVerified = res.IsVerified,
            IsActive = res.IsActive,
            CreatedAt = res.CreatedAt
        });
    }
}