using System.Text.Json;

using CoinGecko.Api.Features.Coins;

namespace CoinGecko.Api.Common;

internal static class HttpResponseMessageExtensions
{
    public static async Task<T?> ReadContentAsAsync<T>(this HttpResponseMessage response)
    {
        await using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        stream.ResetStreamPosition();
        return await JsonSerializer.DeserializeAsync<T>(stream).ConfigureAwait(false);
    }

    public static T? ReadContentAs<T>(this HttpResponseMessage response)
    {
        using var stream = response.Content.ReadAsStream();
        stream.ResetStreamPosition();
        return JsonSerializer.Deserialize<T>(stream);
    }

    public static void ResetStreamPosition(this Stream stream) => stream.Position = 0;
}
