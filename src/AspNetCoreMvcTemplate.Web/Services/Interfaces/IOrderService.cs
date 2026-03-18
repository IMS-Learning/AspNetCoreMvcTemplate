using AspNetCoreMvcTemplate.Web.Models.Domain;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;

namespace AspNetCoreMvcTemplate.Web.Services.Interfaces;

public interface IOrderService
{
    Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetUserOrdersAsync(int userId, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<Order> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);
    Task DeleteOrderAsync(int id, CancellationToken cancellationToken = default);
}
