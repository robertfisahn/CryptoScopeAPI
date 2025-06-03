using CryptoScopeAPI.Models;

namespace CryptoScopeAPI.Services
{
    public interface ICoinGeckoClient
    {
        Task<List<Coin>> GetTopMarketCoinsAsync();
        Task<List<SearchCoin>> GetSearchCoinsAsync();
    }
}
