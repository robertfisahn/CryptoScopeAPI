namespace CryptoScopeAPI.Models;
public class Coin
{
    public int Id { get; set; }
    public string CoinId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Symbol { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public decimal CurrentPriceUsd { get; set; }
    public decimal MarketCapUsd { get; set; }
    public double PriceChangePercentage24h { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}

