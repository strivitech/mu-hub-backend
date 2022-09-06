using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using MuHub.Domain.Common.Entities;

namespace MuHub.Infrastructure.Identity;

public class User : IdentityUser
{
    public bool IsBlocked { get; set; }

    public string RoleName { get; set; } = null!;

    public DateTimeOffset? CreatedAt { get; set; }
    
    public DateTimeOffset? LastLoginAt { get; set; } 
}
