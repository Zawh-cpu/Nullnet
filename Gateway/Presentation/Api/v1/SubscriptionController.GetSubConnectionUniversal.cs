using Microsoft.AspNetCore.Mvc;

namespace Gateway.Presentation.Api.v1;

public partial class SubscriptionController
{
    [HttpGet("/u/{token}")]
    public async Task<IActionResult> GetSubConnectionUniversal()
    {
        
    }
}