namespace MuHub.IdentityProvider.Configurations.Auth;

/// <summary>
/// Cookie authentication configuration.
/// </summary>
public static class CookieAuthenticationConfiguration
{
    public const string CookieName = "MuHub.IdentityProvider";
    public const string LoginPath = "/Account/Login";
    public const string LogoutPath = "/Account/Logout";
}
