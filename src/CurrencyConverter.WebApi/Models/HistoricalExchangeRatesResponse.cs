using System.Text.Json.Serialization;

namespace CurrencyConverter.WebApi.Models;

public class HistoricalExchangeRatesResponse
{
    [JsonPropertyName("rates")]
    public Dictionary<DateOnly, Dictionary<string, decimal>> Rates { get; set; }
}
