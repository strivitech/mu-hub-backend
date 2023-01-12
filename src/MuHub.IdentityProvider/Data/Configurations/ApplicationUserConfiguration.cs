using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MuHub.IdentityProvider.Models;

namespace MuHub.IdentityProvider.Data.Configurations;

/// <summary>
/// Configuration for the <see cref="ApplicationUser"/> entity.
/// </summary>
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}
