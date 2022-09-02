using Microsoft.AspNetCore.Mvc;

using MuHub.Application.Models.Requests.Info;
using MuHub.Application.Services.Interfaces;

namespace MuHub.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class InfoController : ControllerBase
{
    private readonly IInfoService _infoService;
    
    public InfoController(IInfoService infoService)
    {
        _infoService = infoService ?? throw new ArgumentNullException(nameof(infoService));
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateInfoRequest request)
    {
        await _infoService.CreateAsync(request);
        
        return Ok();
    }
}
