using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebAppCore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExchangeRatesController : Controller
{
    [HttpGet(Name = "latest")]
    public async Task<Dictionary<string, decimal>> GetLatestExchangeRatesAsync(string currency)
    {
        return default;
    }

    [HttpGet(Name = "historical")]
    public async Task<Dictionary<DateOnly, Dictionary<string, decimal>>> GetHistoricalExchangeRatesAsync(
        string currency, DateOnly startDate, DateOnly endDate)
    {
        return default;
    }

    [HttpGet(Name = "convert")]
    public async Task<decimal> ConvertAsync(decimal amount, string currentCurrency, string targetCurrency)
    {
        return default;
    }
}
