using System.Net.Http.Json;
using System.Text.Json;
using WebApp.Clients.Interfaces;

namespace WebApp.Clients;

public class ExternalApiClient : IExternalApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ExternalApiClient> _logger;

    public ExternalApiClient(HttpClient httpClient, ILogger<ExternalApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("GET {Endpoint}", endpoint);
        var response = await _httpClient.GetAsync(endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("POST {Endpoint}", endpoint);
        var response = await _httpClient.PostAsJsonAsync(endpoint, data, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("PUT {Endpoint}", endpoint);
        var response = await _httpClient.PutAsJsonAsync(endpoint, data, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("DELETE {Endpoint}", endpoint);
        var response = await _httpClient.DeleteAsync(endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
