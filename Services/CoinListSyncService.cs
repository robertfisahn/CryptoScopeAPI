using Microsoft.Extensions.Options;
using CryptoScopeAPI.Dtos;
using AutoMapper;
using CryptoScopeAPI.Models;

namespace CryptoScopeAPI.Services;
public class CoinListSyncService(IServiceProvider _provider, ILogger<CoinListSyncService> _logger, IOptions<CoinSyncSettings> _settings, IMapper _mapper) : BackgroundService
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

                var freshCoins = await coingecko.GetTopMarketCoinsAsync();
                var mappedCoins = _mapper.Map<List<Coin>>(freshCoins);

                db.Coins.RemoveRange(db.Coins);
                await db.Coins.AddRangeAsync(mappedCoins, stoppingToken);
                await db.SaveChangesAsync(stoppingToken);

                _logger.LogInformation("Top coin list refreshed at {Time}", DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to refresh top coin list");
            }

            await Task.Delay(TimeSpan.FromSeconds(coinSyncSettings.TopListRefreshSeconds), stoppingToken);
        }
    }
}
