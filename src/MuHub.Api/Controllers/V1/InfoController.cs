using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using MuHub.Application.Models.Requests.Info;
using MuHub.Application.Services.Interfaces;

namespace MuHub.Api.Controllers.V1;

[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
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
        return Ok("V1");
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateInfoRequest request)
    {
        await _infoService.CreateAsync(request);

        return Ok();
    }
}
