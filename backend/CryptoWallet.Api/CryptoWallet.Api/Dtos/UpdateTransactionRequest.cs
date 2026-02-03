namespace CryptoWallet.Api.Dtos
{
    public class UpdateTransactionRequest
    {
        public string? CryptoCode { get; set; } 
        public string? Action { get; set; }
        public decimal? CryptoAmount { get; set; }
        public decimal? Money { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
