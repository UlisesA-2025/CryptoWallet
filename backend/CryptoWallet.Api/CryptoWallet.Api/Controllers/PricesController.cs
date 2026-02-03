using CryptoWallet.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWallet.Api.Controllers
{
    [ApiController]
    [Route("prices")]
    public class PricesController : ControllerBase
    {
        private readonly CryptoYaClient _cryptoYa;

        public PricesController(CryptoYaClient cryptoYa)
        {
            _cryptoYa = cryptoYa;
        }

        [HttpGet("{exchange}/{cryptoCode}")]
        public async Task<IActionResult> Get(string exchange, string cryptoCode)
        {
            try
            {
                var price = await _cryptoYa.GetArsPriceAsync(exchange, cryptoCode);
                return Ok(new { exchange, cryptoCode, arsPrice = price });
            }
            catch (Exception ex)
            {
                return StatusCode(502, new
                {
                    message = "No se pudo obtener el precio desde CriptoYa.",
                    detail = ex.Message
                });
            }
        }
    
    }
}
