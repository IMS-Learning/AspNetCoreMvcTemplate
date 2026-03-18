using WebApp.Models.Domain;
using WebApp.Models.DTOs.Requests;
using WebApp.Services.Interfaces;

namespace WebApp.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;

    // In a real application, inject a repository or DbContext here.
    private static readonly List<Product> _products = [];
    private static int _nextId = 1;

    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger;
    }

    public async Task<Product?> GetProductAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving product {ProductId}", id);
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            _logger.LogWarning("Product {ProductId} not found", id);
        return await Task.FromResult(product);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Retrieving products page {Page} with size {PageSize}", page, pageSize);
        var products = _products
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        return await Task.FromResult(products);
    }

    public async Task<int> GetProductCountAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_products.Count);
    }

    public async Task<Product> CreateProductAsync(UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating product {ProductName}", request.Name);

        var product = new Product
        {
            Id = _nextId++,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockLevel = request.StockLevel,
            Category = request.Category,
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        _products.Add(product);
        _logger.LogInformation("Product {ProductId} created successfully", product.Id);
        return await Task.FromResult(product);
    }

    public async Task<Product> UpdateProductAsync(int id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating product {ProductId}", id);
        var product = _products.FirstOrDefault(p => p.Id == id)
            ?? throw new KeyNotFoundException($"Product {id} not found.");

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.StockLevel = request.StockLevel;
        product.Category = request.Category;
        product.UpdatedDate = DateTime.UtcNow;
        return await Task.FromResult(product);
    }

    public async Task DeleteProductAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting product {ProductId}", id);
        var product = _products.FirstOrDefault(p => p.Id == id)
            ?? throw new KeyNotFoundException($"Product {id} not found.");
        _products.Remove(product);
        await Task.CompletedTask;
    }
}
