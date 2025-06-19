using System.Text.Json.Serialization;

namespace CryptoScopeAPI.Dtos.CoinGecko
{
    public class CoinSearchGeckoResponse
    {
        [JsonPropertyName("id")] public string Id { get; set; } = default!;
        [JsonPropertyName("symbol")] public string Symbol { get; set; } = default!;
        [JsonPropertyName("name")] public string Name { get; set; } = default!;
    }
}
