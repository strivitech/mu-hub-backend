using ErrorOr;

using MuHub.Shared.Constants.Api;

namespace MuHub.Api.ApiErrors;

/// <summary>
/// 
/// </summary>
public static partial class ErrorsScope
{
    /// <summary>
    /// 
    /// </summary>
    public static class Auth
    {
        /// <summary>
        /// 
        /// </summary>
        public static Error PasswordSignInFailed => Error.Custom(
                ErrorTypeCodes.Status401Unauthorized, 
                "Auth.PasswordSignInFailed", 
                "User was not found");
    }   
}
