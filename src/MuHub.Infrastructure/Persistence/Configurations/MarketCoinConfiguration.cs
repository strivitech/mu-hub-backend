using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MuHub.Domain.Entities;

namespace MuHub.Infrastructure.Persistence.Configurations;

public class MarketCoinConfiguration : IEntityTypeConfiguration<MarketCoin>
{
    public void Configure(EntityTypeBuilder<MarketCoin> builder)
    {
        builder.ToTable(TableNames.MarketCoins);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SymbolId).IsRequired();
        builder.Property(x => x.Symbol).IsRequired();
        builder.Property(x => x.Name).IsRequired();
    }
}
