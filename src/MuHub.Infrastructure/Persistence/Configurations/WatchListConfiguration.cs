using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MuHub.Domain.Entities;

namespace MuHub.Infrastructure.Persistence.Configurations;

public class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
{
    public void Configure(EntityTypeBuilder<WatchList> builder)
    {
        builder.ToTable(TableNames.WatchLists);
        builder.HasKey(x => new { x.CoinId, x.UserId });
    }
}