namespace WebApp.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
    Task SendOrderConfirmationAsync(int orderId, string recipientEmail, CancellationToken cancellationToken = default);
    Task SendPasswordResetAsync(string email, string resetToken, CancellationToken cancellationToken = default);
}
