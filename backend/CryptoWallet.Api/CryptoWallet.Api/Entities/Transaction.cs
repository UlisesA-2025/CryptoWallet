namespace CryptoWallet.Api.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string CryptoCode { get; set; } = default!;
        public string Action { get; set; } = default!; // "purchase" or "sell"
        public decimal CryptoAmount { get; set; }
        public decimal Money { get; set; }
        public DateTime DateTime { get; set; }

    }
}
