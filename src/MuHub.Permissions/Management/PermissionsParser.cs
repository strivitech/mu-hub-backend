namespace MuHub.Permissions.Management;

/// <summary>
/// Parses a string of permissions into other forms.
/// </summary>
public static class PermissionsParser
{
    /// <summary>
    /// Gets an enumerable of <see cref="Permission"/> from the string.
    /// </summary>
    /// <param name="permissions">String of permissions.</param>
    /// <returns>Enumerable of <see cref="Permission"/>.</returns>
    public static IEnumerable<Permission> GetFromString(this string permissions)
    {
        if (string.IsNullOrWhiteSpace(permissions))
        {
            yield break;
        }

        foreach (var character in permissions)
        {
            yield return (Permission)character;
        }
    }
}
