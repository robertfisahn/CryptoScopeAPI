using CryptoScopeAPI.Models;
using System.Text.Json.Serialization;

namespace CryptoScopeAPI.Services
{
    public class CoinGeckoClient(HttpClient _http) : ICoinGeckoClient
    {
        public async Task<List<Coin>> GetTopMarketCoinsAsync()
        {
            var response = await _http.GetFromJsonAsync<List<CoinGeckoResponse>>(
                "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1&sparkline=false"
            );

            return response!.Select(c => new Coin
            {
                CoinId = c.Id,
                Name = c.Name,
                Symbol = c.Symbol,
                ImageUrl = c.Image,
                CurrentPriceUsd = c.CurrentPrice,
                MarketCapUsd = c.MarketCap,
                PriceChangePercentage24h = c.PriceChangePercentage24h
            }).ToList();
        }

        private class CoinGeckoResponse
        {
            [JsonPropertyName("id")]
            public string Id { get; set; } = default!;

            [JsonPropertyName("name")]
            public string Name { get; set; } = default!;

            [JsonPropertyName("symbol")]
            public string Symbol { get; set; } = default!;

            [JsonPropertyName("image")]
            public string Image { get; set; } = default!;

            [JsonPropertyName("current_price")]
            public decimal CurrentPrice { get; set; }

            [JsonPropertyName("price_change_percentage_24h")]
            public double PriceChangePercentage24h { get; set; }

            [JsonPropertyName("market_cap")]
            public decimal MarketCap { get; set; }
        }
    }
}
