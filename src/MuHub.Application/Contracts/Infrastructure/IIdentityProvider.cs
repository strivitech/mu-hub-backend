using MuHub.Application.Models.Responses;

namespace MuHub.Application.Contracts.Infrastructure;

/// <summary>
/// 
/// </summary>
public interface IIdentityProvider
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    Task<GetIdentityProviderUserResponse> GetIdentityProviderUserAsync(string userName);
}
