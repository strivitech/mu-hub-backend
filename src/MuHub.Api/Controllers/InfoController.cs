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

[ApiController]
[Route("[controller]/[action]")]
public class InfoController2 : ControllerBase
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

[ApiController]
[Route("[controller]/[action]")]
public class InfoController3 : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        var s = "String";
        return Ok(s);
    }
    
    [HttpGet]
    public IActionResult Index2()
    {
        var s = "String";
        return Ok();
    }
}