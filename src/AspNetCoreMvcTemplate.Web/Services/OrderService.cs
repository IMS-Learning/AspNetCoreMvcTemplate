using AspNetCoreMvcTemplate.Web.Models.Domain;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;
using AspNetCoreMvcTemplate.Web.Services.Interfaces;

namespace AspNetCoreMvcTemplate.Web.Services;

public class OrderService : IOrderService
{
    private readonly IProductService _productService;
    private readonly ILogger<OrderService> _logger;

    // In a real application, inject a repository or DbContext here.
    private static readonly List<Order> _orders = [];
    private static int _nextId = 1;

    public OrderService(IProductService productService, ILogger<OrderService> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public async Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving order {OrderId}", id);
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
            _logger.LogWarning("Order {OrderId} not found", id);
        return await Task.FromResult(order);
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Retrieving orders for user {UserId}", userId);
        var orders = _orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        return await Task.FromResult(orders);
    }

    public async Task<Order> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating order for product {ProductId}", request.ProductId);

        var product = await _productService.GetProductAsync(request.ProductId, cancellationToken)
            ?? throw new KeyNotFoundException($"Product {request.ProductId} not found.");

        if (request.Quantity > product.StockLevel)
            throw new InvalidOperationException("Not enough stock available.");

        var total = product.Price * request.Quantity;

        var order = new Order
        {
            Id = _nextId++,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
            Total = total,
            Status = "Pending",
            CreatedDate = DateTime.UtcNow
        };

        _orders.Add(order);
        _logger.LogInformation("Order {OrderId} created successfully", order.Id);
        return await Task.FromResult(order);
    }

    public async Task DeleteOrderAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting order {OrderId}", id);
        var order = _orders.FirstOrDefault(o => o.Id == id)
            ?? throw new KeyNotFoundException($"Order {id} not found.");
        _orders.Remove(order);
        await Task.CompletedTask;
    }
}
