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

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new("muhubapi", "MuHub API")
            {
                Scopes =
                {
                    Configurations.Clients.ApiScopes.MuHubApiRead,
                    Configurations.Clients.ApiScopes.MuHubApiWrite,
                    Configurations.Clients.ApiScopes.MuHubApiDelete
                }
            },
        };

    // M2M clients
    public static IEnumerable<Client> M2MClients =>
        Enumerable.Empty<Client>();


    // interactive client using code flow + pkce
    public static IEnumerable<Client> CodeWithPkceClients(IEnumerable<CodeWithPkceClients> clients) =>
        clients.Select(newClient => new Client
        {
            ClientId = newClient.ClientId,
            AccessTokenType = AccessTokenType.Jwt,
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
