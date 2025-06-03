namespace CryptoScopeAPI.Models
{
    public class SearchCoin
    {
        public int Id { get; set; }
        public string CoinId { get; set; } = default!;
        public string Symbol { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

}
