using Microsoft.AspNetCore.Authorization;

namespace MuHub.Permissions.Policy;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permissionName)
    {
        PermissionName = permissionName;
    }

    public string PermissionName { get; }
}
