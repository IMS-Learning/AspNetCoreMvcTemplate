using WebApp.Models.Domain;
using WebApp.Models.DTOs.Requests;

namespace WebApp.Services.Interfaces;

public interface IProductService
{
    Task<Product?> GetProductAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetProductsAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<int> GetProductCountAsync(CancellationToken cancellationToken = default);
    Task<Product> CreateProductAsync(UpdateProductRequest request, CancellationToken cancellationToken = default);
    Task<Product> UpdateProductAsync(int id, UpdateProductRequest request, CancellationToken cancellationToken = default);
    Task DeleteProductAsync(int id, CancellationToken cancellationToken = default);
}
