using Microsoft.AspNetCore.Authorization;

namespace MuHub.Permissions.Policy;

/// <summary>
/// Custom authorization requirement.
/// </summary>
public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permissionName)
    {
        PermissionName = permissionName;
    }

    /// <summary>
    /// Name of permission.
    /// </summary>
    public string PermissionName { get; }
}
