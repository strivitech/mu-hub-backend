namespace CoinGecko.Api.Endpoints;

public static class QueryParametersConstants
{
    public static class Coin
    {
        public static class List
        {
            public const string IncludePlatform = "include_platform";
        }
        
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
