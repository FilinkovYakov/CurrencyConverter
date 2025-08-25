using CurrencyConverter.WebApi.Models;

namespace CurrencyConverter.WebApi.Services;

public interface IExchangeRatesService
{
    Task<Dictionary<string, decimal>> GetLatestExchangeRatesAsync(string currentCurrency);
    Task<decimal> GetLatestTargetExchangeRateAsync(string currentCurrency, string targetCurrency);
    Task<Dictionary<DateOnly, Dictionary<string, decimal>>> GetHistoricalExchangeRatesAsync(
        string currentCurrency, DateOnly startDate, DateOnly endDate);
}

class ExchangeRatesService : IExchangeRatesService
{
    readonly IHttpClientService _httpClientService;

    public ExchangeRatesService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async Task<Dictionary<string, decimal>> GetLatestExchangeRatesAsync(string currentCurrency)
    {
        var response = await _httpClientService.GetAsync<LatestExchangeRatesResponse>(
            "v1/latest", $"base={currentCurrency?.ToUpperInvariant()}");
        return response.Rates;
    }

    public async Task<decimal> GetLatestTargetExchangeRateAsync(string currentCurrency, string targetCurrency)
    {
        var response = await _httpClientService.GetAsync<LatestExchangeRatesResponse>(
            "v1/latest", $"base={currentCurrency?.ToUpperInvariant()}&symbols={targetCurrency?.ToUpperInvariant()}");
        if (response.Rates?.Count != 1)
            throw new InvalidOperationException($"Rates for {targetCurrency} have not been found. Target currency is {currentCurrency}");
        return response.Rates.FirstOrDefault().Value;
    }

    public async Task<Dictionary<DateOnly, Dictionary<string, decimal>>> GetHistoricalExchangeRatesAsync(
        string currentCurrency, DateOnly startDate, DateOnly endDate)
    {
        var response = await _httpClientService.GetAsync<HistoricalExchangeRatesResponse>(
            $"v1/{startDate:yyyy-MM-dd}..{endDate:yyyy-MM-dd}", $"base={currentCurrency?.ToUpperInvariant()}");
        return response.Rates;
    }
}
