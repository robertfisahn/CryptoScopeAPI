using CryptoScopeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoScopeAPI;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Coin> Coins => Set<Coin>();
    public DbSet<SearchCoin> SearchCoins => Set<SearchCoin>();
    public DbSet<CoinDetails> CoinDetails => Set<CoinDetails>();
    public DbSet<CoinMarketChart> CoinMarketCharts => Set<CoinMarketChart>();
}