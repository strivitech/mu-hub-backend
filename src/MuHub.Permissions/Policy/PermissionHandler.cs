using Microsoft.AspNetCore.Authorization;

using MuHub.IdentityProvider.Shared.Claims;
using MuHub.Permissions.Management;

namespace MuHub.Permissions.Policy;

/// <summary>
/// Custom authorization handler that operates with permissions.
/// </summary>
public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissionsClaim =
            context.User.Claims.SingleOrDefault(c => c.Type == IdentityProviderClaimTypes.Permissions);

        if (permissionsClaim?.Value.VerifyPermissionByRequirement(requirement) ?? false)
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}
