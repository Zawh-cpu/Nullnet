using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

[ApiController]
[Route("api/[controller]")]
public partial class RoleController : ControllerBase
{
    private readonly ISender _sender;
    
    public RoleController(ISender sender)
    {
        _sender = sender;
    }
}