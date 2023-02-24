using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Application.Mapping;

public static class ToMarketCoinMapper
{
    public static MarketCoin ToMarketCoin(this MarketCoinDto dto)
    {
        return new MarketCoin
        {
            SymbolId = dto.Id,
            Symbol = dto.Symbol,
            Name = dto.Name,
            ImageUrl = dto.ImageUrl,
            CurrentPrice = dto.CurrentPrice,
            High24H = dto.High24H,
            Low24H = dto.Low24H,
            LastUpdated = dto.LastUpdated,
        };
    }

    public static List<MarketCoin> ToMarketCoins(this IEnumerable<MarketCoinDto> dtos)
    {
        return dtos.Select(ToMarketCoin).ToList();
    }
}
