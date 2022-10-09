using System.Security.Claims;

using MuHub.Application.Contracts.Infrastructure;

namespace MuHub.Api.Services;

/// <summary>
/// 
/// </summary>
public class CurrentUserSessionData : IUserSessionData
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public CurrentUserSessionData(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public string UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) 
                            ?? throw new InvalidOperationException("User id not found because user is not authenticated");
}
