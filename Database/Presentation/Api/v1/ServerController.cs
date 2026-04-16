using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

[ApiController]
[Route("api/[controller]")]
public partial class ServerController : ControllerBase
{
    private readonly ISender _sender;

    public ServerController(ISender sender)
    {
        _sender = sender;
    }
}

