using Duende.IdentityServer;
using Duende.IdentityServer.Models;

using MuHub.IdentityProvider.Configurations.Clients;

namespace MuHub.IdentityProvider;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope(Configurations.Clients.ApiScopes.MuHubApiRead),
            new ApiScope(Configurations.Clients.ApiScopes.MuHubApiWrite),
            new ApiScope(Configurations.Clients.ApiScopes.MuHubApiDelete)
        };

    // interactive client using code flow + pkce
    public static IEnumerable<Client> CodeWithPkceClients(IEnumerable<CodeWithPkceClients> clients) =>
        clients.Select(newClient => new Client
        {
            ClientId = newClient.ClientId,
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            RequireClientSecret = false,
            RedirectUris = newClient.RedirectUris,
            PostLogoutRedirectUris = newClient.PostLogoutRedirectUris,
            AllowOfflineAccess = true,
            AllowedCorsOrigins = newClient.AllowedCorsOrigins,
            AllowedScopes = newClient.AllowedScopes,
            AllowAccessTokensViaBrowser = true,
            RequireConsent = false,
        });
}
