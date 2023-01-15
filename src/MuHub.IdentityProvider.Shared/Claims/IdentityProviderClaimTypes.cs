using IdentityModel;

namespace MuHub.IdentityProvider.Shared.Claims;

/// <summary>
/// Contains the names of the claims used by the identity provider.
/// </summary>
public static class IdentityProviderClaimTypes
{
    /// <summary>
    /// Contains the 'sub' name.
    /// </summary>
    public const string Sub = JwtClaimTypes.Subject;

    /// <summary>
    /// Contains the 'role' name.
    /// </summary>
    public const string Role = JwtClaimTypes.Role;

    /// <summary>
    /// Contains the 'permissions' name.
    /// </summary>
    public const string Permissions = "permissions";
}
