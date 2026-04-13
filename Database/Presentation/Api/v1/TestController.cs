using Database.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

[Route("api/[controller]")]
public class TestController : Controller
{
    [HttpGet("")]
    public IActionResult GetException()
    {
        throw new ImTeapotException();
    }
}