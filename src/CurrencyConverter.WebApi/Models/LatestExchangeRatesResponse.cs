using System.Text.Json.Serialization;

namespace CurrencyConverter.WebApi.Models;

public class LatestExchangeRatesResponse
{
    [JsonPropertyName("rates")]
    public Dictionary<string, decimal> Rates { get; set; }
}
