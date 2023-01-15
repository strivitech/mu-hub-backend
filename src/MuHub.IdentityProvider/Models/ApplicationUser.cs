// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;

namespace MuHub.IdentityProvider.Models;

/// <summary>
/// Application user.
/// </summary>
public class ApplicationUser : IdentityUser
{
    public string RoleName { get; set; }
    
    public bool IsBlocked { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
}
