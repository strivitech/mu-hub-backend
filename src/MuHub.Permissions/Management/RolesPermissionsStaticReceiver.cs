using System.Runtime.Serialization;
using System.Text.Json;

using MuHub.Permissions.Data;

namespace MuHub.Permissions.Management;

public static class RolesPermissionsStaticReceiver
{
    private static readonly Dictionary<Role, IRolePermissionsGroup> RolesPermissionsGroups =
        InitializeRolePermissionsGroups();

    static RolesPermissionsStaticReceiver()
    {
    }

    public static bool TryGetPermissionsByRole(Role role, out IReadOnlyCollection<Permission> permissions)
    {
        var result = RolesPermissionsGroups.TryGetValue(role, out var group);
        permissions = group?.Permissions ?? Array.Empty<Permission>();
        return result;
    }
    
    public static Dictionary<Role, IRolePermissionsGroup> GetAllRolePermissionsGroups 
        => CreateDictionaryGroupsDeepCopy(RolesPermissionsGroups);

    private static Dictionary<Role, IRolePermissionsGroup> InitializeRolePermissionsGroups()
    {
        var rolePermissionsTypeInfoEnumerable = typeof(IAssemblyMarker).Assembly.DefinedTypes
            .Where(x => x is { IsClass: true, IsAbstract: false } && typeof(IRolePermissionsGroup).IsAssignableFrom(x));

        var rolePermissionsGroups = rolePermissionsTypeInfoEnumerable
            .Select(typeInfo => (IRolePermissionsGroup)Activator.CreateInstance(typeInfo)!)
            .ToDictionary(rolePermissionsGroup => rolePermissionsGroup.Role);
        
        return rolePermissionsGroups;
    }

    private static Dictionary<Role, IRolePermissionsGroup> CreateDictionaryGroupsDeepCopy(
        Dictionary<Role, IRolePermissionsGroup> source)
    {
        var json = JsonSerializer.Serialize(source);
        return JsonSerializer.Deserialize<Dictionary<Role, IRolePermissionsGroup>>(json) ??
               throw new SerializationException("Unable to properly deserialize dictionary");
    }
}
