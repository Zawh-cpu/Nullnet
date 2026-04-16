using Database.Application.UseCases.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

public partial class RoleController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var res = await _sender.Send(new GetRoleQuery(id), cancellationToken);

        return Ok(new
        {
            Id = res.Id,
            Code = res.Code,
            Name = res.Name,
            IsDefault = res.IsDefault
        });
    }
}

