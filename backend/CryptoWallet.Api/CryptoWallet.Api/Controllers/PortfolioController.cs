using CryptoWallet.Api.Data;
using CryptoWallet.Api.Dtos;
using CryptoWallet.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Api.Controllers;

[ApiController]
[Route("portfolio")]
public class PortfolioController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly CryptoYaClient _cryptoYa;

    public PortfolioController(AppDbContext db, CryptoYaClient cryptoYa)
    {
        _db = db;
        _cryptoYa = cryptoYa;
    }

    [HttpGet]
    public async Task<ActionResult<PortfolioResponse>> Get()
    {
        // 1) Traemos transacciones (lo mínimo)
        var trans = await _db.Transactions
            .Select(t => new { t.CryptoCode, t.Action, t.CryptoAmount })
            .ToListAsync();

        // 2) Holdings en memoria (C#)
        var holdings = trans
            .GroupBy(t => t.CryptoCode)
            .Select(g => new
            {
                Crypto = g.Key,
                Holding = g.Sum(t => t.Action == "purchase" ? t.CryptoAmount : -t.CryptoAmount)
            })
            .Where(x => x.Holding > 0)
            .ToList();

        // 3) Valuación con CriptoYa
        const string exchange = "binance";

        var resp = new PortfolioResponse();

        foreach (var h in holdings)
        {
            var price = await _cryptoYa.GetArsPriceAsync(exchange, h.Crypto);
            var money = price * h.Holding;

            resp.Items.Add(new PortfolioItem
            {
                CryptoCode = h.Crypto,
                Amount = h.Holding,
                Money = money
            });

            resp.Total += money;
        }

        return Ok(resp);
    }
}
