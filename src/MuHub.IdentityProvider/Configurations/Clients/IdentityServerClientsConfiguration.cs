namespace MuHub.IdentityProvider.Configurations.Clients;

public class IdentityServerClientsConfiguration
{
    public const string SectionName = "IdentityServer:Clients";
    
    public CodeWithPkceClients[] CodeWithPkce { get; set; }
}
