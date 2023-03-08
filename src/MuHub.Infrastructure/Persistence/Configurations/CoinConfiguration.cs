using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MuHub.Domain.Entities;

namespace MuHub.Infrastructure.Persistence.Configurations;

public class CoinConfiguration : IEntityTypeConfiguration<Coin>
{
    public void Configure(EntityTypeBuilder<Coin> builder)
    {
        builder.ToTable(TableNames.Coins);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ExternalSymbolId).IsRequired();
        builder.Property(x => x.SymbolId).IsRequired();
        builder.Property(x => x.Symbol).IsRequired();
        builder.Property(x => x.Name).IsRequired();
    }
}
