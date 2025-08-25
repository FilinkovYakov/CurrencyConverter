using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CurrencyConverter.WebApi.Tests.Controllers;

[TestFixture]
[Explicit]
class ExchangeRatesControllerIntegrationTests
{
    WebApplicationFactory<Program> _factory;
    HttpClient _httpClient;

    [SetUp]
    public void SetUp()
    {
        _factory = new WebApplicationFactory<Program>();
        _httpClient = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _httpClient?.Dispose();
        _factory?.Dispose();
    }

    [Test]
    public async Task GetLatestExchangeRatesTestAsync()
    {
        //ACT
        var response = await _httpClient.GetAsync("/api/ExchangeRates/latest/?currency=USD");

        //ASSERT
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, decimal>>();
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(result.ContainsKey("JPY"), Is.True);
    }

    [Test]
    public async Task GetHistoricalExchangeRatesTestAsync()
    {
        //ACT
        var response = await _httpClient.GetAsync("/api/ExchangeRates/historical/?currency=USD&startDate=1999-12-30&endDate=2000-12-29");

        //ASSERT
        var result = await response.Content.ReadFromJsonAsync<Dictionary<DateOnly, Dictionary<string, decimal>>>();
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(result.Count, Is.EqualTo(256));
    }

    [Test]
    public async Task ConvertTestAsync()
    {
        //ACT
        var response = await _httpClient.GetAsync("/api/ExchangeRates/convert/?amount=13&currentCurrency=USD&targetCurrency=EUR");

        //ASSERT
        var result = await response.Content.ReadFromJsonAsync<decimal>();
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
