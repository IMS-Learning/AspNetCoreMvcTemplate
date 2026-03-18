namespace WebApp.Models.DTOs.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}
