using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MuHub.Domain.Entities;

namespace MuHub.Infrastructure.Persistence.Configurations;

public class InfoConfiguration : IEntityTypeConfiguration<Info>
{
    public void Configure(EntityTypeBuilder<Info> builder)
    {
        throw new NotImplementedException();
    }
}
