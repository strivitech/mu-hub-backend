using System.Text.Json;

using CoinGecko.Api.Features.Coins;

namespace CoinGecko.Api.Common;

/// <summary>
/// Extension methods for <see cref="HttpResponseMessage"/>.
/// </summary>
internal static class HttpResponseMessageExtensions
{
    /// <summary>
    /// Asynchronously reads the content of the response as a <typeparamref name="T"/>.
    /// </summary>
    /// <param name="response">Response.</param>
    /// <typeparam name="T">Type to cast.</typeparam>
    /// <returns>An instance of <typeparam name="T"></typeparam> or null.</returns>
    public static async Task<T?> ReadContentAsAsync<T>(this HttpResponseMessage response)
    {
        await using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        stream.ResetStreamPosition();
        return await JsonSerializer.DeserializeAsync<T>(stream).ConfigureAwait(false);
    }

    /// <summary>
    /// Reads the content of the response as a <typeparamref name="T"/>.
    /// </summary>
    /// <param name="response">Response.</param>
    /// <typeparam name="T">Type to cast.</typeparam>
    /// <returns>An instance of <typeparam name="T"></typeparam> or null.</returns>
    public static T? ReadContentAs<T>(this HttpResponseMessage response)
    {
        using var stream = response.Content.ReadAsStream();
        stream.ResetStreamPosition();
        return JsonSerializer.Deserialize<T>(stream);
    }

    /// <summary>
    /// Resets the stream position to 0.
    /// </summary>
    /// <param name="stream">Stream.</param>
    public static void ResetStreamPosition(this Stream stream) => stream.Position = 0;
}
