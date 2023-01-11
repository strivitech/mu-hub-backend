// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Identity;

namespace MuHub.IdentityProvider.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public bool IsBlocked { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
}
