using MuHub.Application.Contracts.Infrastructure;

namespace MuHub.Api.Services;

/// <summary>
/// 
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userSessionData"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public CurrentUserService(IUserSessionData userSessionData)
    {
        UserSessionData = userSessionData ?? throw new ArgumentNullException(nameof(userSessionData));
    }

    /// <summary>
    /// 
    /// </summary>
    public IUserSessionData UserSessionData { get; }
}  
