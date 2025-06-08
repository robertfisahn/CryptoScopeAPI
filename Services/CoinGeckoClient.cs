using AutoMapper;
using CryptoScopeAPI.Dtos;
using CryptoScopeAPI.Dtos.CoinGecko;
using CryptoScopeAPI.Models;
using System.Text.Json.Serialization;

namespace CryptoScopeAPI.Services
{
    public class CoinGeckoClient(HttpClient _http, IMapper _mapper) : ICoinGeckoClient
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

        public async Task<List<SearchCoin>> GetSearchCoinsAsync()
        {
            var response = await _http.GetFromJsonAsync<List<SearchCoinGeckoResponse>>(
                "https://api.coingecko.com/api/v3/coins/list"
            );

            return response!.Select(c => new SearchCoin
            {
                CoinId = c.Id,
                Name = c.Name,
                Symbol = c.Symbol
            }).ToList();
        }

        private class SearchCoinGeckoResponse
        {
            [JsonPropertyName("id")] public string Id { get; set; } = default!;
            [JsonPropertyName("symbol")] public string Symbol { get; set; } = default!;
            [JsonPropertyName("name")] public string Name { get; set; } = default!;
        }

        public async Task<CoinDetailsDto> GetCoinDetailsAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _http.GetFromJsonAsync<CoinDetailsGeckoResponse>(
                $"https://api.coingecko.com/api/v3/coins/{id}",
                cancellationToken
            );
            if (response == null)
            {
                return null!;
            }
            return _mapper.Map<CoinDetailsDto>(response);
        }

        public async Task<CoinMarketChartGeckoResponse> GetCoinMarketChartAsync(string id, string days, CancellationToken cancellationToken)
        {
            var response = await _http.GetFromJsonAsync<CoinMarketChartGeckoResponse>(
                $"https://api.coingecko.com/api/v3/coins/{id}/market_chart?vs_currency=usd&days={days}",
                cancellationToken
            );
            if (response == null)
            {
                return null!;
            }
            return response;
        }
    }
}
