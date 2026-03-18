using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.DTOs.Requests;

public class UpdateProductRequest
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int StockLevel { get; set; }

    [StringLength(100)]
    public string Category { get; set; } = string.Empty;
}
