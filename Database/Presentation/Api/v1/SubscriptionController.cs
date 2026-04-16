using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Database.Presentation.Api.v1;

[ApiController]
[Route("api/[controller]")]
public partial class SubscriptionController : ControllerBase
{
    private readonly ISender _sender;

    public SubscriptionController(ISender sender)
    {
        _sender = sender;
    }
}

