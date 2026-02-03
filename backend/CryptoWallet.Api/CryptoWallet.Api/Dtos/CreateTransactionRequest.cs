using System.ComponentModel.DataAnnotations;

namespace CryptoWallet.Api.Dtos
{
    public class CreateTransactionRequest
    {

        [Required]
        public string CryptoCode { get; set; } = default!;

        [Required]
        public string Action { get; set; } = default!;

        [Range(0.0000000001, double.MaxValue)]
        public decimal CryptoAmount { get; set; }

        public DateTime DateTime { get; set; }
    }
}
