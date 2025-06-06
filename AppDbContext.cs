using Microsoft.EntityFrameworkCore;
using CryptoScopeAPI.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Coin> Coins => Set<Coin>();
    public DbSet<SearchCoin> SearchCoins => Set<SearchCoin>();
    public DbSet<CoinDetails> CoinDetails => Set<CoinDetails>();

}