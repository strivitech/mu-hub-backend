using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MuHub.IdentityProvider;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    // public static IEnumerable<ApiResource> ApiResources => new[]
    // {
    //     new ApiResource("muhubapi")
    //     {
    //         Scopes = new List<string> {"muhubapi.read", "muhubapi.write", "muhubapi.delete"},
    //         ApiSecrets = new List<Secret> { new("511536EF-F270-4058-81CA-1C89C192F69A".Sha256()) },
    //         UserClaims = new List<string> {"role", "permissions"},
    //     },
    // };
    
    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("muhubapi.read"),
            new ApiScope("muhubapi.write"),
            new ApiScope("muhubapi.delete")
        };

    // TODO: take secrets outside of method
    public static IEnumerable<Client> Clients =>
        new[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("muhubapisecret".Sha256()) },
                AllowedScopes =
                {
                    "muhubapi.read",
                    "muhubapi.write",
                    "muhubapi.delete"
                }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                RedirectUris = { "https://localhost:3000/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:3000/signout-callback-oidc" },
                AllowOfflineAccess = true,
                AllowedCorsOrigins = { "https://localhost:3000" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "muhubapi.read",
                    "muhubapi.write",
                    "muhubapi.delete"
                },
                AllowAccessTokensViaBrowser = true,
                RequireConsent = false,
            },
        };
}
