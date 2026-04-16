using Gateway.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Presentation.Api.v1;

[ApiController]
[Route("api/[controller]")]
public class TestController : Controller
{
    [HttpGet("")]
    public IActionResult GetException()
    {
        throw new ImTeapotException();
    }
}