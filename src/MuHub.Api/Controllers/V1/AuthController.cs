using System.Net;

using Asp.Versioning;

using ErrorOr;

using Microsoft.AspNetCore.Mvc;

using MuHub.Api.ApiErrors;
using MuHub.Api.ApiModels.Login;
using MuHub.Api.ApiModels.Registration;
using MuHub.Api.Services;
using MuHub.Shared.Constants.Api;

namespace MuHub.Api.Controllers.V1;

/// <summary>
/// 
/// </summary>
[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : MuControllerBase
{
    private readonly IAuthService _authService;
    
    /// <summary>
    /// 
    /// </summary>
    public AuthController(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var loginResult = await _authService.RegisterAsync(request);
        return loginResult.Match(Ok, Problem);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        var loginResult = await _authService.LoginAsync(request);
        return loginResult.Match(
            Ok,
            errors =>
            {
                var firstError = errors.First();
                return firstError.NumericType == ErrorTypeCodes.Status401Unauthorized
                ? Problem(firstError, statusCode: StatusCodes.Status401Unauthorized)
                : Problem(errors);
            });
    }
}
