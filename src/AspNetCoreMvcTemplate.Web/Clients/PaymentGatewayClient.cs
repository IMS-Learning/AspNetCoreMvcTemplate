using AspNetCoreMvcTemplate.Web.Clients.Interfaces;
using AspNetCoreMvcTemplate.Web.Models.Domain;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;

namespace AspNetCoreMvcTemplate.Web.Clients;

public class PaymentGatewayClient : IPaymentGatewayClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PaymentGatewayClient> _logger;

    public PaymentGatewayClient(HttpClient httpClient, ILogger<PaymentGatewayClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Payment> ChargeAsync(ProcessPaymentRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Processing payment for order {OrderId}", request.OrderId);

        // In a real implementation, this would call the actual payment gateway API.
        await Task.CompletedTask;

        return new Payment
        {
            OrderId = request.OrderId,
            Amount = request.Amount,
            Method = request.Method,
            Status = "Completed",
            TransactionId = Guid.NewGuid().ToString(),
            CreatedDate = DateTime.UtcNow
        };
    }

    public async Task<Payment?> GetTransactionAsync(string transactionId, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Fetching transaction {TransactionId}", transactionId);
        await Task.CompletedTask;
        return null;
    }

    public async Task RefundAsync(string transactionId, decimal amount, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Refunding {Amount} for transaction {TransactionId}", amount, transactionId);
        await Task.CompletedTask;
    }
}
