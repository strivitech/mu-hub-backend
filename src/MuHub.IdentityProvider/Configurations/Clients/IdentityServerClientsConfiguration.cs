namespace MuHub.IdentityProvider.Configurations.Clients;

/// <summary>
/// IdentityServer clients configuration.
/// </summary>
public class IdentityServerClientsConfiguration
{
    public const string SectionName = "IdentityServer:Clients";
    
    /// <summary>
    /// Clients that require code with PKCE flow.
    /// </summary>
    public CodeWithPkceClient[] CodeWithPkce { get; set; }
}
