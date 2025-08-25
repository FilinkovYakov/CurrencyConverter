using CurrencyConverter.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.WebAppCore.Controllers;

[ApiController]
public class ExchangeRatesController : Controller
{
    readonly IExchangeRatesService _exchangeRatesService;

    public ExchangeRatesController(IExchangeRatesService exchangeRatesService)
    {
        _exchangeRatesService = exchangeRatesService;
    }

    [HttpGet]
    [Route("api/[controller]/latest")]
    public async Task<Dictionary<string, decimal>> GetLatestExchangeRatesAsync(string currency)
    {
        return await _exchangeRatesService.GetLatestExchangeRatesAsync(currency);
    }

    [HttpGet]
    [Route("api/[controller]/historical")]
    public async Task<Dictionary<DateOnly, Dictionary<string, decimal>>> GetHistoricalExchangeRatesAsync(
        string currency, DateOnly startDate, DateOnly endDate)
    {
        return await _exchangeRatesService.GetHistoricalExchangeRatesAsync(currency, startDate, endDate);
    }

    [HttpGet]
    [Route("api/[controller]/convert")]
    public async Task<decimal> ConvertAsync(decimal amount, string currentCurrency, string targetCurrency)
    {
        return await _exchangeRatesService.GetLatestTargetExchangeRateAsync(currentCurrency, targetCurrency) * amount;
    }
}
