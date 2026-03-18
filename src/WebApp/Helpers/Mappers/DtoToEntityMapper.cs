using WebApp.Models.Domain;
using WebApp.Models.DTOs.Requests;
using WebApp.Models.ViewModels;

namespace WebApp.Helpers.Mappers;

public static class DtoToEntityMapper
{
    public static User ToUser(CreateUserRequest request) => new()
    {
        FirstName = request.FirstName,
        LastName = request.LastName,
        Email = request.Email,
        Username = request.Username,
        CreatedDate = DateTime.UtcNow
    };

    public static Product ToProduct(UpdateProductRequest request) => new()
    {
        Name = request.Name,
        Description = request.Description,
        Price = request.Price,
        StockLevel = request.StockLevel,
        Category = request.Category,
        IsActive = true,
        CreatedDate = DateTime.UtcNow
    };

    public static CreateUserRequest ToRequest(CreateUserViewModel viewModel) => new()
    {
        FirstName = viewModel.FirstName,
        LastName = viewModel.LastName,
        Email = viewModel.Email,
        Username = viewModel.Username,
        Password = viewModel.Password
    };
}
