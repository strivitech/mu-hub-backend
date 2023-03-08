using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Application.Mapping;

public static class ToMarketCoinMapper
{
    public static MarketCoin ToMarketCoin(this MarketCoinDto dto, Coin projection)
    {
        return new MarketCoin
        {
            SymbolId = projection.SymbolId,
            Name = projection.Name,
            Symbol = projection.Symbol,
            ImageUrl = dto.ImageUrl,
            CurrentPrice = dto.CurrentPrice,
            MarketCapRank = dto.MarketCapRank,
            High24H = dto.High24H,
            Low24H = dto.Low24H,
            LastUpdated = dto.LastUpdated,
            IsValid = dto.IsValid
        };
    }

    public static List<MarketCoin> ToMarketCoins(this IEnumerable<MarketCoinDto> dtos,
        IDictionary<string, Coin> projections) 
        => dtos.Select(x => x.ToMarketCoin(projections[x.Id])).ToList();
}
