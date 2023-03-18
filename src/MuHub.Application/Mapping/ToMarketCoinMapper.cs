using MuHub.Domain.Entities;
using MuHub.Market.Proxy.Features.Coins;

namespace MuHub.Application.Mapping;

/// <summary>
/// This class is used to map an instance to MarketCoin.
/// </summary>
public static class ToMarketCoinMapper
{
    /// <summary>
    /// This method is used to map <see cref="MarketCoinDto"/> to <see cref="MarketCoin"/>.
    /// </summary>
    /// <param name="dto">MarketCoinDto.</param>
    /// <param name="projection">An additional projection of Coin.</param>
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

    /// <summary>
    /// This method is used to map a enumerable of <see cref="MarketCoinDto"/> to list of <see cref="MarketCoin"/>.
    /// </summary>
    /// <param name="dtos">MarketCoinDto.</param>
    /// <param name="projections">An additional projection of Coin.</param>
    public static List<MarketCoin> ToMarketCoins(this IEnumerable<MarketCoinDto> dtos,
        IDictionary<string, Coin> projections) 
        => dtos.Select(x => x.ToMarketCoin(projections[x.Id])).ToList();
}
