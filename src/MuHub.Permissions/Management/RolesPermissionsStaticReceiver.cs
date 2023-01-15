using System.Collections.Immutable;
using System.Runtime.Serialization;
using System.Text.Json;

using MuHub.Permissions.Data;

namespace MuHub.Permissions.Management;

/// <summary>
/// Static receiver that retrieves roles and permissions.
/// </summary>
public static class RolesPermissionsStaticReceiver
{
    static RolesPermissionsStaticReceiver()
    {
    }

    /// <summary>
    /// Tries to get an <see cref="IReadOnlyCollection{T}"/> of <see cref="Permission"/> by <see cref="Role"/>.
    /// </summary>
    /// <param name="role">Role.</param>
    /// <param name="permissions">A read only collection of permissions.</param>
    /// <returns>True if permissions by role were found, otherwise - false.</returns>
    public static bool TryGetPermissionsByRole(Role role, out IReadOnlyCollection<Permission> permissions)
    {
        var result = GetAllRolePermissionsGroups.TryGetValue(role, out var group);
        permissions = group?.Permissions ?? Array.Empty<Permission>();
        return result;
    }
    
    /// <summary>
    /// Gets all role-permission groups.
    /// </summary>
    public static ImmutableDictionary<Role, IRolePermissionsGroup> GetAllRolePermissionsGroups { get; } = CreateRolePermissionsGroups();

    private static ImmutableDictionary<Role, IRolePermissionsGroup> CreateRolePermissionsGroups()
    {
        var rolePermissionsTypeInfoEnumerable = typeof(IAssemblyMarker).Assembly.DefinedTypes
            .Where(x => x is { IsClass: true, IsAbstract: false } && typeof(IRolePermissionsGroup).IsAssignableFrom(x));

        var rolePermissionsGroups = rolePermissionsTypeInfoEnumerable
            .Select(typeInfo => (IRolePermissionsGroup)Activator.CreateInstance(typeInfo)!)
            .ToImmutableDictionary(rolePermissionsGroup => rolePermissionsGroup.Role);
        
        return rolePermissionsGroups;
    }
}
