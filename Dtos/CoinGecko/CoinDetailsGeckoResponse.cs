using System.Text.Json.Serialization;

namespace CryptoScopeAPI.Dtos.CoinGecko
{
    public class CoinDetailsGeckoResponse
    {
        [JsonPropertyName("id")] 
        public string Id { get; set; } = default!;
        
        [JsonPropertyName("symbol")] 
        public string Symbol { get; set; } = default!;

        [JsonPropertyName("name")] 
        public string Name { get; set; } = default!;

        [JsonPropertyName("image")]  
        public CoinImage Image { get; set; } = default!;

        [JsonPropertyName("market_data")] 
        public CoinMarketData? MarketData { get; set; }

        public class CoinImage
        {
            [JsonPropertyName("thumb")] 
            public string Thumb { get; set; } = default!;

            [JsonPropertyName("small")] 
            public string Small { get; set; } = default!;

            [JsonPropertyName("large")] 
            public string Large { get; set; } = default!;
        }

        public class CoinMarketData
        {
            [JsonPropertyName("current_price")] 
            public UsdValue? CurrentPrice { get; set; }

            [JsonPropertyName("market_cap")] 
            public UsdValue? MarketCap { get; set; }

            [JsonPropertyName("price_change_percentage_24h")] 
            public double? PriceChangePercentage24h { get; set; }
        }

        public class UsdValue
        {
            [JsonPropertyName("usd")] 
            public decimal? Usd { get; set; }
        }
    }
}
