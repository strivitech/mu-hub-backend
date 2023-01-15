using System.Security.Claims;

using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MuHub.IdentityProvider.Data;
using MuHub.IdentityProvider.Models;
using MuHub.IdentityProvider.Shared.Claims;
using MuHub.Permissions;

using Serilog;

namespace MuHub.IdentityProvider.Services;

/// <summary>
/// This class allows IdentityServer to connect to your user and profile store.
/// </summary>
public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _applicationDbContext;

    public ProfileService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
    {
        _userManager = userManager;
        _applicationDbContext = applicationDbContext;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        Claim roleClaim = context.Subject.FindFirst(claim => claim.Type == IdentityProviderClaimTypes.Role);

        if (roleClaim is null)
        {
            throw new InvalidOperationException("User does not have a role claim");
        }
        
        var permissionsClaim = new Claim(IdentityProviderClaimTypes.Permissions,
            await GetRequiredUserPermissionsByRoleName(roleClaim.Value));

        var claims = new List<Claim>
        {
            roleClaim,
            permissionsClaim,
        };

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        if (user is null)
        {
            Log.Error("User not found for sub {Sub}", sub);
        }

        context.IsActive = user is not null && !user.IsBlocked;
    }
    
    private async Task<string> GetRequiredUserPermissionsByRoleName(string roleName)
    {
        RolePermissions permissionsForUser = await _applicationDbContext.
            RolePermissions.FirstAsync(x => x.RoleName == roleName);

        return permissionsForUser.PermissionsString;
    }
}
