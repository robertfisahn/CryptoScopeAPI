namespace CryptoScopeAPI.Models
{
    public class CoinMarketChart
    {
        public int Id { get; set; }
        public string CoinId { get; set; } = default!;
        public string TimeRange { get; set; } = default!;
        public string PricesJson { get; set; } = default!;
        public string MarketCapsJson { get; set; } = default!;
        public string TotalVolumesJson { get; set; } = default!;
        public DateTime LastUpdated { get; set; }
    }
}
