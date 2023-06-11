using System.Security.Claims;

namespace MuHub.IdentityProvider.Shared.Claims;

public static class UserClaimsExtensions
{
    public static Claim GetClaimByType(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        return claimsPrincipal.FindFirst(claimType) ??
               throw new InvalidOperationException($"Claim with type {claimType} not found");
    }

    public static string GetClaimValueByType(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        return claimsPrincipal.FindFirst(claimType)?.Value ??
               throw new InvalidOperationException($"Claim with type {claimType} not found");
    }
    
    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValueByType(IdentityProviderClaimTypes.Sub);
    }
}
