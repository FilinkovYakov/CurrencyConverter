using CurrencyConverter.WebApi.Services;

namespace CurrencyConverter.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IExchangeRatesService, ExchangeRatesService>();
        builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
        builder.Services.AddControllers();
        builder.Services.AddHttpClient();

        var app = builder.Build();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}