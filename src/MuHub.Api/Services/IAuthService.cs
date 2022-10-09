using ErrorOr;

using MuHub.Api.ApiModels;
using MuHub.Api.ApiModels.Login;
using MuHub.Api.ApiModels.Registration;

namespace MuHub.Api.Services;

/// <summary>
/// 
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ErrorOr<RegisterUserResponse>> RegisterAsync(RegisterUserRequest request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ErrorOr<LoginUserResponse>> LoginAsync(LoginUserRequest request);
}
