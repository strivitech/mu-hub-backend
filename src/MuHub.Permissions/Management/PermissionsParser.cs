namespace MuHub.Permissions.Management;

public static class PermissionsParser
{
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
