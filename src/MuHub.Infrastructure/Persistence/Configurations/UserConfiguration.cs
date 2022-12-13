using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MuHub.Domain.Entities;

namespace MuHub.Infrastructure.Persistence.Configurations;

// TODO: Add configuration for the appropriate properties:
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Email)
            .IsRequired();

        builder.Property(x => x.UserName)
            .IsRequired();
            
        builder.Property(x => x.RoleName)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .IsRequired();
        
        builder.Property(x => x.IdentityProviderId)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}
