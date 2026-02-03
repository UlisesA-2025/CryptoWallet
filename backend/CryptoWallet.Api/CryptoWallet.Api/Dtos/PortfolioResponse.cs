namespace CryptoWallet.Api.Dtos
{
    public class PortfolioItem
    {
        public string CryptoCode { get; set; } = default!;
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
    }

    public class PortfolioResponse
    {
        public List<PortfolioItem> Items { get; set; } = new();
        public decimal Total { get; set; }
    }
}

