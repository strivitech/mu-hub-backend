namespace CoinGecko.Api.Endpoints;

/// <summary>
/// Contains coin endpoints.
/// </summary>
public static class EndpointsMap
{
    public static class Coin
    {   
        public const string Coins = "coins";
        public const string CoinList = "coins/list";
        public const string CoinMarkets = "coins/markets";
        public static string AllDataByCoinId(string id) => AddCoinsIdUrl(id);
        public static string TickerByCoinId(string id) => AddCoinsIdUrl(id) + "/tickers";
        public static string HistoryByCoinId(string id) => AddCoinsIdUrl(id) + "/history";
        public static string MarketChartByCoinId(string id) => AddCoinsIdUrl(id) + "/market_chart";
        public static string MarketChartRangeByCoinId(string id) => AddCoinsIdUrl(id) + "/market_chart/range";
        public static string StatusUpdates(string id) => AddCoinsIdUrl(id) + "/status_updates";
        public static string CoinOhlc(string id) => AddCoinsIdUrl(id) + "/ohlc";
    
        public static string AddCoinsIdUrl(string id) => "coins/" + id;   
    }
}
