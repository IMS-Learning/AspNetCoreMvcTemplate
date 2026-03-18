using WebApp.Models.DTOs.Responses;

namespace WebApp.Models.ViewModels;

public class DashboardViewModel
{
    public int TotalUsers { get; set; }
    public int TotalProducts { get; set; }
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public IEnumerable<OrderResponse> RecentOrders { get; set; } = [];
}
