using AspNetCoreMvcTemplate.Web.Models.Domain;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Responses;

namespace AspNetCoreMvcTemplate.Web.Helpers.Mappers;

public static class EntityToDtoMapper
{
    public static UserResponse ToResponse(User user) => new()
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        Username = user.Username,
        Role = user.Role,
        IsActive = user.IsActive,
        CreatedDate = user.CreatedDate
    };

    public static ProductResponse ToResponse(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description,
        Price = product.Price,
        StockLevel = product.StockLevel,
        Category = product.Category,
        IsActive = product.IsActive
    };

    public static OrderResponse ToResponse(Order order, string productName = "") => new()
    {
        Id = order.Id,
        UserId = order.UserId,
        ProductId = order.ProductId,
        ProductName = productName,
        Quantity = order.Quantity,
        Total = order.Total,
        Status = order.Status,
        CreatedDate = order.CreatedDate
    };
}
