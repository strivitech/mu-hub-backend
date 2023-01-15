namespace MuHub.Permissions.Management;

/// <summary>
/// Creates a new permissions into convenient form.
/// </summary>
public static class PermissionsCreator
{
    /// <summary>
    /// Creates a string of permissions. Each char value of this string represents a <see cref="Permission"/>.
    /// </summary>
    /// <param name="permissions">Enumerable of <see cref="Permission"/>.</param>
    /// <returns>A string contains all permissions.</returns>
    public static string CreateIntoString(this IEnumerable<Permission> permissions) =>
        permissions.Aggregate(string.Empty, (s, permission) => s + (char)permission);
}
