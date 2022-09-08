using Asp.Versioning;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using MuHub.Api.Contracts.Requests;
using MuHub.Api.Services;
using MuHub.Infrastructure.Contracts.Requests;
using MuHub.Infrastructure.Identity;

namespace MuHub.Api.Controllers.V1;

/// <summary>
/// 
/// </summary>
[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authService"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public AuthController(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        return result.Match<IActionResult>(
            BadRequest,
            Ok);
    }
}
