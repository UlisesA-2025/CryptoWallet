using CryptoWallet.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Api.Services
{
    public class HoldingsService
    {

        private readonly AppDbContext _db;

        public HoldingsService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<decimal> GetHoldingAsync(string cryptoCode)
        {
            var trans = await _db.Transactions
            .Where(t => t.CryptoCode == cryptoCode)
            .Select(t => new { t.Action, t.CryptoAmount })
            .ToListAsync();

            return trans.Sum(t => t.Action == "purchase" ? t.CryptoAmount : -t.CryptoAmount);
        }
    }
}
