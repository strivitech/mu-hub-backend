namespace MuHub.Permissions.Management;

public static class PermissionsCreator
{
    public static string CreateIntoString(this IEnumerable<Permission> permissions) =>
        permissions.Aggregate(string.Empty, (s, permission) => s + (char)permission);
}
