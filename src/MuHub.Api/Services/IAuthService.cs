using MuHub.Api.Requests;
using MuHub.Api.Responses;

namespace MuHub.Api.Services;

/// <summary>
/// Service for authentication and authorization behavior.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<UserLinkRegistrationDataResponse?> RelateUserRegistrationFlowAsync(UserLinkRegistrationDataRequest request);
}
