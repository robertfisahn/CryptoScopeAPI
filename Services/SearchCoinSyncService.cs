using CryptoScopeAPI.Dtos;
using Microsoft.Extensions.Options;

namespace CryptoScopeAPI.Services
{
    public class SearchCoinSyncService(IServiceProvider _provider, ILogger<SearchCoinSyncService> _logger, IOptions<CoinSyncSettings> _settings) : BackgroundService
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

                    var fresh = await coingecko.GetSearchCoinsAsync();

                    db.SearchCoins.RemoveRange(db.SearchCoins);
                    await db.SearchCoins.AddRangeAsync(fresh, stoppingToken);
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
