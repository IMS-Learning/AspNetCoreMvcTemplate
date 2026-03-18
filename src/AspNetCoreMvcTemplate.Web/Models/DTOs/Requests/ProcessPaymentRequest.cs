using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;

public class ProcessPaymentRequest
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(50)]
    public string Method { get; set; } = string.Empty;
}
