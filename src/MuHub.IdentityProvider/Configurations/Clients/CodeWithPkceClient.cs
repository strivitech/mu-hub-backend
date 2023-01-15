namespace MuHub.IdentityProvider.Configurations.Clients;

/// <summary>
/// Code with PKCE client configuration.
/// </summary>
public class CodeWithPkceClient
{
    public string ClientId { get; set; }
    public string[] RedirectUris { get; set; }
    public string[] PostLogoutRedirectUris { get; set; }
    public string[] AllowedCorsOrigins { get; set; }
    public string[] AllowedScopes { get; set; }
}
