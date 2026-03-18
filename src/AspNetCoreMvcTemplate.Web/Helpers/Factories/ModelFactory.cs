using AspNetCoreMvcTemplate.Web.Models.DTOs.Responses;
using AspNetCoreMvcTemplate.Web.Models.ViewModels;

namespace AspNetCoreMvcTemplate.Web.Helpers.Factories;

public static class ModelFactory
{
    public static UserListViewModel CreateUserListViewModel(
        IEnumerable<UserResponse> users,
        int totalCount,
        int page,
        int pageSize) => new()
    {
        Users = users,
        TotalCount = totalCount,
        Page = page,
        PageSize = pageSize
    };

    public static DashboardViewModel CreateDashboardViewModel(
        int totalUsers,
        int totalProducts,
        int totalOrders,
        decimal totalRevenue,
        IEnumerable<OrderResponse> recentOrders) => new()
    {
        TotalUsers = totalUsers,
        TotalProducts = totalProducts,
        TotalOrders = totalOrders,
        TotalRevenue = totalRevenue,
        RecentOrders = recentOrders
    };
}
