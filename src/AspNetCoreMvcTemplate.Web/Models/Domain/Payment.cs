namespace AspNetCoreMvcTemplate.Web.Models.Domain;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}
