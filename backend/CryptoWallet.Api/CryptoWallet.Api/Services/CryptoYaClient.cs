using System.Text.Json;

namespace CryptoWallet.Api.Services
{
    public class CryptoYaClient
    {

        private readonly HttpClient _http;

        public CryptoYaClient(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://criptoya.com/api/");
        }

        public async Task<decimal> GetArsPriceAsync(string exchange, string cryptoCode)
        {
            var res = await _http.GetAsync($"{exchange}/{cryptoCode}/ars");

            var body = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
                throw new InvalidOperationException($"CriptoYa devolvió {(int)res.StatusCode}. Body: {body}");

            var trimmed = body.TrimStart();
            if (trimmed.Length == 0 || (trimmed[0] != '{' && trimmed[0] != '['))
                throw new InvalidOperationException($"CriptoYa no devolvió JSON. Body: {body}");

            using var doc = JsonDocument.Parse(body);

            if (doc.RootElement.TryGetProperty("bid", out var bid))
                return bid.GetDecimal();

            if (doc.RootElement.TryGetProperty("ask", out var ask))
                return ask.GetDecimal();

            throw new InvalidOperationException($"No se encontró bid/ask. Body: {body}");
        }

    }
}
