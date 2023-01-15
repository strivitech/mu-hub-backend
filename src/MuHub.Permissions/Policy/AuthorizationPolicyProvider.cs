using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace MuHub.Permissions.Policy;

/// <summary>
/// Custom authorization policy provider.
/// </summary>
public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets <see cref="AuthorizationPolicy"/> by name.
    /// </summary>
    /// <param name="policyName">Policy name.</param>
    /// <returns>An instance of <see cref="AuthorizationPolicy"/>.</returns>
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        return await base.GetPolicyAsync(policyName)
               ?? new AuthorizationPolicyBuilder()
                   .AddRequirements(new PermissionRequirement(policyName))
                   .Build();
    }
}
