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
            Name = dto.Id,//a
            Symbol = dto.Id,//a
            ImageUrl = dto.ImageUrl,
            CurrentPrice = dto.CurrentPrice,
            MarketCapRank = dto.MarketCapRank,
            High24H = dto.High24H,
            Low24H = dto.Low24H,
            LastUpdated = dto.LastUpdated,
            IsValid = dto.IsValid
        };
    }

    public static List<MarketCoin> ToMarketCoins(this IEnumerable<MarketCoinDto> dtos)
    {
        return dtos.Select(ToMarketCoin).ToList();
    }
}
