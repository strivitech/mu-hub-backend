using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

namespace MuHub.Api.Controllers.V2;

[ApiController]
[ApiVersion(2.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public class InfoController : MuControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("V2");
    }
}
