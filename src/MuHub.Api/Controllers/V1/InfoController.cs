using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

namespace MuHub.Api.Controllers.V1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public class InfoController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("V1");
    }
}
