using AspNetCoreMvcTemplate.Web.Models.Domain;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;

namespace AspNetCoreMvcTemplate.Web.Services.Interfaces;

public interface IPaymentService
{
    Task<Payment> ProcessPaymentAsync(ProcessPaymentRequest request, CancellationToken cancellationToken = default);
    Task<Payment?> GetPaymentAsync(int id, CancellationToken cancellationToken = default);
    Task RefundPaymentAsync(int paymentId, CancellationToken cancellationToken = default);
}
