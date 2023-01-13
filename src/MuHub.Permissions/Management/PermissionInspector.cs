using System.ComponentModel;

using MuHub.Permissions.Policy;

namespace MuHub.Permissions.Management;

public static class PermissionInspector
{
    public static bool VerifyPermissionByRequirement(this string permissions, PermissionRequirement requirement)
    {
        if (!Enum.TryParse(requirement.PermissionName, true, out Permission permissionToVerify))
        {
            throw new InvalidEnumArgumentException(
                $"{requirement.PermissionName} can't be converted to a {nameof(Permissions)}.");
        }
        
        var usersPermissions = permissions.GetFromString().ToHashSet();

        return usersPermissions.UserHasPermission(permissionToVerify);
    }

    public static bool UserHasPermission(this IEnumerable<Permission> usersPermissions, Permission permissionToVerify) 
        => usersPermissions.Contains(permissionToVerify);
}
