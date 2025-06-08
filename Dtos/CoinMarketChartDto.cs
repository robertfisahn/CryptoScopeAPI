using System.Text.Json.Serialization;

namespace CryptoScopeAPI.Dtos
{
    public class CoinMarketChartDto
    {
        public string CoinId { get; set; } = default!;
        public string TimeRange { get; set; } = default!;

        [JsonPropertyName("prices")]
        public List<List<decimal>> Prices { get; set; } = [];

        [JsonPropertyName("market_caps")]
        public List<List<decimal>> MarketCaps { get; set; } = [];

        [JsonPropertyName("total_volumes")]
        public List<List<decimal>> TotalVolumes { get; set; } = [];
    }
}
