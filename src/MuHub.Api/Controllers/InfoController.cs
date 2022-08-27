using Microsoft.AspNetCore.Mvc;

namespace MuHub.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class InfoController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
    
    [HttpGet]
    public IActionResult Index2()
    {
        return Ok();
    }
}