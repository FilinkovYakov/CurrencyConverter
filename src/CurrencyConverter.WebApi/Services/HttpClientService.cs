namespace CurrencyConverter.WebApi.Services;

interface IHttpClientService
{
    Task<T> GetAsync<T>(string path, string query);
}

class HttpClientService : IHttpClientService
{
    const string _scheme = "http";
    const string _host = "api.frankfurter.dev";
    readonly IHttpClientFactory _httpClientFactory;

    public HttpClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<T> GetAsync<T>(string path, string query)
    {
        var builder = new UriBuilder();
        builder.Scheme = _scheme;
        builder.Host = _host;
        builder.Path = path;
        builder.Query = query;
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync(builder.Uri);
        return await response.Content.ReadFromJsonAsync<T>();
    }
}
