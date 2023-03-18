namespace CoinGecko.Api.Endpoints;

/// <summary>
/// Query parameters constants.
/// </summary>
public static class QueryParametersConstants
{
    /// <summary>
    /// Contains coin query parameters.
    /// </summary>
    public static class Coin
    {
        /// <summary>
        /// For List of coins.
        /// </summary>
        public static class List
        {
            public const string IncludePlatform = "include_platform";
        }
        
        /// <summary>
        /// For Market Coins.
        /// </summary>
        public static class Market
        {
            public const string VsCurrency = "vs_currency";
            public const string Ids = "ids";
            public const string Category = "category";
            public const string Order = "order";
            public const string PerPage = "per_page";
            public const string Page = "page";
            public const string Sparkline = "sparkline";
            public const string PriceChangePercentage = "price_change_percentage";
        }
    }
}
