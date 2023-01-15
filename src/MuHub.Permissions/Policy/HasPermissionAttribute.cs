using Microsoft.AspNetCore.Authorization;

namespace MuHub.Permissions.Policy;

/// <summary>
/// Attribute describes permission to verify.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission)
        : base(policy: permission.ToString())
    {
    }
}
