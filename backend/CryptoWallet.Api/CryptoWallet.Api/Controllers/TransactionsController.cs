using CryptoWallet.Api.Data;
using CryptoWallet.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CryptoWallet.Api.Services;
using CryptoWallet.Api.Dtos;




namespace CryptoWallet.Api.Controllers
{
    [ApiController]
    [Route("transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly CryptoYaClient _cryptoYa;
        private readonly HoldingsService _holdings;

        public TransactionsController(AppDbContext db, CryptoYaClient cryptoYa, HoldingsService holdings)
        {
            _db = db;
            _cryptoYa = cryptoYa;
            _holdings = holdings;
        }

        //GET / transactions    
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Transactions
                .OrderByDescending(t => t.DateTime)
                .ToListAsync();

            return Ok(list);
        }

        //GET /transactions/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trans = await _db.Transactions.FindAsync(id);
            if (trans == null)
            {
                return NotFound();
            }
            return Ok(trans);
        }

        //POST /transactions
        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionRequest req)
        {
            var crypto = req.CryptoCode.Trim().ToLower();
            var action = req.Action.Trim().ToLower();

            if(action is not ("purchase" or "sale"))
            {
                return BadRequest("Action debe ser 'purchase' o 'sale'.");
            }

            if(req.CryptoAmount <= 0)
            {
                return BadRequest("CryptoAmount debe ser mayor que cero.");
            }

            if(action == "sale")
            {
                var holding = await _holdings.GetHoldingAsync(crypto);
                if(req.CryptoAmount > holding)
                {
                    return BadRequest("No podés vender más de lo que tenés.");
                }
            }

            const string exchange = "binance";
            var priceArs = await _cryptoYa.GetArsPriceAsync(exchange, crypto);

            var money = req.CryptoAmount * priceArs;

            var trans = new Transaction
            {
                CryptoCode = crypto,
                Action = action,
                CryptoAmount = req.CryptoAmount,
                Money = money,
                DateTime = req.DateTime == default ? DateTime.UtcNow : req.DateTime
            };

            _db.Transactions.Add(trans);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = trans.Id }, trans);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateTransactionRequest req)
        {
            var trans = await _db.Transactions.FindAsync(id);
            if(trans == null) return NotFound();


            if(req.CryptoCode != null) trans.CryptoCode = req.CryptoCode.Trim().ToLower();
            if(req.Action != null) trans.Action = req.Action.Trim().ToLower();
            if(req.CryptoAmount.HasValue) trans.CryptoAmount = req.CryptoAmount.Value;
            if(req.Money.HasValue) trans.Money = req.Money.Value;
            if(req.DateTime.HasValue) trans.DateTime = req.DateTime.Value;

            await _db.SaveChangesAsync();

            return Ok(trans);
        }
        

        //DELETE /transactions/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var trans = await _db.Transactions.FindAsync(id);
            if(trans == null)
            {
                return NotFound();
            }

            _db.Transactions.Remove(trans);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
