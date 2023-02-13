namespace CoinGecko.Api.Endpoints;

/// <summary>
/// Contains general endpoints info.
/// </summary>
public static class GeneralEndpointsInfo
{
    public static readonly Uri ApiUri = new(ApiUriStringValue);
    public const string ApiUriStringValue = "https://api.coingecko.com/api/v3/";
}
