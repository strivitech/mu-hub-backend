using System.ComponentModel;

using MuHub.Permissions.Policy;

namespace MuHub.Permissions.Management;

/// <summary>
/// Contains methods to inspect permissions.
/// </summary>
public static class PermissionInspector
{
    /// <summary>
    /// Verifies if the given <paramref name="permissions"/> contains essential data to satisfy <paramref name="requirement"/>.
    /// </summary>
    /// <param name="permissions">Permissions string.</param>
    /// <param name="requirement">Permission requirement.</param>
    /// <returns>True if permissions string satisfies requirement, otherwise - false.</returns>
    /// <exception cref="InvalidEnumArgumentException">If unable to cast <paramref name="requirement"/> to <see cref="Permission"/>.</exception>
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

    /// <summary>
    /// Determines whether user has a permission or not.
    /// </summary>
    /// <param name="usersPermissions">Users' permissions.</param>
    /// <param name="permissionToVerify">Permission to verify.</param>
    /// <returns>True if user has this permission, otherwise - false.</returns>
    public static bool UserHasPermission(this IEnumerable<Permission> usersPermissions, Permission permissionToVerify)
        => usersPermissions.Contains(permissionToVerify);
}
