using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MuHub.Domain.Entities;
using MuHub.Infrastructure.Identity;

namespace MuHub.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.RoleName)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}
