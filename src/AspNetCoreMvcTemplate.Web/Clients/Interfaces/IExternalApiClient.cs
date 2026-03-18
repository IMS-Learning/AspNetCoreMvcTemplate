namespace AspNetCoreMvcTemplate.Web.Clients.Interfaces;

public interface IExternalApiClient
{
    Task<T?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default);
    Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data, CancellationToken cancellationToken = default);
    Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data, CancellationToken cancellationToken = default);
    Task DeleteAsync(string endpoint, CancellationToken cancellationToken = default);
}
