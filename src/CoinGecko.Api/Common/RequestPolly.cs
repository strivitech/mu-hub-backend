namespace CoinGecko.Api.Common;

/// <summary>
/// Contains constants for request polly.
/// </summary>
public static class RequestPolly
{
    public const int MedianFirstRetryDelaySeconds = 1;
    public const int DefaultRetryCount = 3;
}
