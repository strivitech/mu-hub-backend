namespace MuHub.IdentityProvider.Configurations;

/// <summary>
/// IdentityServer options configuration.
/// </summary>
public static class IdentityServerOptionsConfiguration
{
    public const bool EmitStaticAudienceClaim = true;
    public const string IssuerUri = "https://localhost:5001";
}
