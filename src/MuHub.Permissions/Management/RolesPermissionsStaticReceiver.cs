using System.Collections.Immutable;
using System.Runtime.Serialization;
using System.Text.Json;

using MuHub.Permissions.Data;

namespace MuHub.Permissions.Management;

public static class RolesPermissionsStaticReceiver
{
    static RolesPermissionsStaticReceiver()
    {
    }

    public static bool TryGetPermissionsByRole(Role role, out IReadOnlyCollection<Permission> permissions)
    {
        var result = GetAllRolePermissionsGroups.TryGetValue(role, out var group);
        permissions = group?.Permissions ?? Array.Empty<Permission>();
        return result;
    }
    
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
