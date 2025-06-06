using CryptoScopeAPI.Dtos;
using CryptoScopeAPI.Dtos.CoinGecko;
using CryptoScopeAPI.Models;

namespace CryptoScopeAPI.Services
{
    public interface ICoinGeckoClient
    {
        Task<List<Coin>> GetTopMarketCoinsAsync();
        Task<List<SearchCoin>> GetSearchCoinsAsync();
        Task<CoinDetailsDto> GetCoinDetailsAsync(string id, CancellationToken token);
        Task<CoinMarketChartGeckoResponse> GetCoinMarketChartAsync(string id, string days, CancellationToken token);
    }
}
