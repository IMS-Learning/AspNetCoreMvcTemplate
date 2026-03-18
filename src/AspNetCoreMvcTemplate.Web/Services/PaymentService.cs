using AspNetCoreMvcTemplate.Web.Clients.Interfaces;
using AspNetCoreMvcTemplate.Web.Models.Domain;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;
using AspNetCoreMvcTemplate.Web.Services.Interfaces;

namespace AspNetCoreMvcTemplate.Web.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentGatewayClient _paymentGatewayClient;
    private readonly ILogger<PaymentService> _logger;

    // In a real application, inject a repository or DbContext here.
    private static readonly List<Payment> _payments = [];
    private static int _nextId = 1;

    public PaymentService(IPaymentGatewayClient paymentGatewayClient, ILogger<PaymentService> logger)
    {
        _paymentGatewayClient = paymentGatewayClient;
        _logger = logger;
    }

    public async Task<Payment> ProcessPaymentAsync(ProcessPaymentRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Processing payment for order {OrderId}", request.OrderId);

        var payment = await _paymentGatewayClient.ChargeAsync(request, cancellationToken);
        payment.Id = _nextId++;
        _payments.Add(payment);

        _logger.LogInformation("Payment {PaymentId} processed successfully for order {OrderId}", payment.Id, request.OrderId);
        return payment;
    }

    public async Task<Payment?> GetPaymentAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Retrieving payment {PaymentId}", id);
        var payment = _payments.FirstOrDefault(p => p.Id == id);
        return await Task.FromResult(payment);
    }

    public async Task RefundPaymentAsync(int paymentId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Refunding payment {PaymentId}", paymentId);
        var payment = _payments.FirstOrDefault(p => p.Id == paymentId)
            ?? throw new KeyNotFoundException($"Payment {paymentId} not found.");

        await _paymentGatewayClient.RefundAsync(payment.TransactionId, payment.Amount, cancellationToken);
        payment.Status = "Refunded";
    }
}
