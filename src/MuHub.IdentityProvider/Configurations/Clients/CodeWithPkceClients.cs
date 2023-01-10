namespace MuHub.IdentityProvider.Configurations.Clients;

public class CodeWithPkceClients
{
    public string ClientId { get; set; }
    public string[] RedirectUris { get; set; }
    public string[] PostLogoutRedirectUris { get; set; }
    public string[] AllowedCorsOrigins { get; set; }
    public string[] AllowedScopes { get; set; }
}
