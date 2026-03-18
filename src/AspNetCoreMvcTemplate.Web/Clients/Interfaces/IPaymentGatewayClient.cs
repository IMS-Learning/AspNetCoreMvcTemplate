using AspNetCoreMvcTemplate.Web.Models.Domain;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;

namespace AspNetCoreMvcTemplate.Web.Clients.Interfaces;

public interface IPaymentGatewayClient
{
    Task<Payment> ChargeAsync(ProcessPaymentRequest request, CancellationToken cancellationToken = default);
    Task<Payment?> GetTransactionAsync(string transactionId, CancellationToken cancellationToken = default);
    Task RefundAsync(string transactionId, decimal amount, CancellationToken cancellationToken = default);
}
