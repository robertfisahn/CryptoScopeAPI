using CryptoScopeAPI.Dtos.CoinGecko;

namespace CryptoScopeAPI.Services
{
    public class CoinGeckoClient(HttpClient _http) : ICoinGeckoClient
    {
        public async Task<List<CoinListGeckoResponse>> GetTopMarketCoinsAsync()
        {
            var response = await _http.GetFromJsonAsync<List<CoinListGeckoResponse>>(
                "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=10&page=1&sparkline=false"
            );
            if (response == null)
            {
                return null!;
            }
            return response;
        }

        public async Task<List<CoinSearchGeckoResponse>> GetSearchCoinsAsync()
        {
            var response = await _http.GetFromJsonAsync<List<CoinSearchGeckoResponse>>(
                "https://api.coingecko.com/api/v3/coins/list"
            );

            if (response == null)
            {
                return null!;
            }
            return response;
        }

        public async Task<CoinDetailsGeckoResponse> GetCoinDetailsAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _http.GetFromJsonAsync<CoinDetailsGeckoResponse>(
                $"https://api.coingecko.com/api/v3/coins/{id}",
                cancellationToken
            );
            if (response == null)
            {
                return null!;
            }
            return response;
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
