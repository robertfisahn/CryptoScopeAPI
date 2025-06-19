using CryptoScopeAPI.Dtos;
using CryptoScopeAPI.Dtos.CoinGecko;
using CryptoScopeAPI.Models;

namespace CryptoScopeAPI.Services
{
    public interface ICoinGeckoClient
    {
        Task<List<CoinListGeckoResponse>> GetTopMarketCoinsAsync();
        Task<List<CoinSearchGeckoResponse>> GetSearchCoinsAsync();
        Task<CoinDetailsGeckoResponse> GetCoinDetailsAsync(string id, CancellationToken token);
        Task<CoinMarketChartGeckoResponse> GetCoinMarketChartAsync(string id, string days, CancellationToken token);
    }
}
