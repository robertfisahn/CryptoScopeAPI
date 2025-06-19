using AutoMapper;
using CryptoScopeAPI.Dtos;
using CryptoScopeAPI.Models;
using Microsoft.Extensions.Options;

namespace CryptoScopeAPI.Services
{
    public class SearchCoinSyncService(IServiceProvider _provider, ILogger<SearchCoinSyncService> _logger, IOptions<CoinSyncSettings> _settings, IMapper _mapper) : BackgroundService
    {
        private readonly CoinSyncSettings coinSyncSettings = _settings.Value;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _provider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var coingecko = scope.ServiceProvider.GetRequiredService<ICoinGeckoClient>();

                    var freshCoins = await coingecko.GetSearchCoinsAsync();
                    var mappedCoins = _mapper.Map<List<SearchCoin>>(freshCoins);

                    db.SearchCoins.RemoveRange(db.SearchCoins);
                    await db.SearchCoins.AddRangeAsync(mappedCoins, stoppingToken);
                    await db.SaveChangesAsync(stoppingToken);

                    _logger.LogInformation("Search coins list refreshed at {Time}", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to refresh search coin list");
                }

                await Task.Delay(TimeSpan.FromSeconds(coinSyncSettings.SearchRefreshSeconds), stoppingToken);
            }
        }
    }
}
