using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

[ApiController]
[Route("api/[controller]")]
public partial class UserController : ControllerBase
{
    private readonly ISender _sender;
    
    public UserController(ISender sender)
    {
        _sender = sender;
    }
}