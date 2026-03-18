using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;

public class CreateOrderRequest
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
