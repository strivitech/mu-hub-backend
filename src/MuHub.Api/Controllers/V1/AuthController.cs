using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using MuHub.Api.Requests;
using MuHub.Api.Services;

namespace MuHub.Api.Controllers.V1;

/// <summary>
/// 
/// </summary>
[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class AuthController : MuControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authService"></param>
    public AuthController(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> RelateUserRegistrationFlow(UserLinkRegistrationDataRequest request)
    {
        var userLinkResponse = await _authService.RelateUserRegistrationFlowAsync(request);
        return Ok(userLinkResponse);
    }
}
