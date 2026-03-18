using WebApp.Models.Domain;
using WebApp.Models.DTOs.Requests;

namespace WebApp.Services.Interfaces;

public interface IPaymentService
{
    Task<Payment> ProcessPaymentAsync(ProcessPaymentRequest request, CancellationToken cancellationToken = default);
    Task<Payment?> GetPaymentAsync(int id, CancellationToken cancellationToken = default);
    Task RefundPaymentAsync(int paymentId, CancellationToken cancellationToken = default);
}
