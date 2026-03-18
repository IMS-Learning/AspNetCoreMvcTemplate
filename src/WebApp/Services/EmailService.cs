using Microsoft.Extensions.Options;
using WebApp.Models.Options;
using WebApp.Services.Interfaces;

namespace WebApp.Services;

public class EmailService : IEmailService
{
    private readonly SmtpOptions _smtpOptions;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IOptions<SmtpOptions> smtpOptions, ILogger<EmailService> logger)
    {
        _smtpOptions = smtpOptions.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending email to {Recipient} with subject '{Subject}'", to, subject);

        // In a real implementation, use SmtpClient or a third-party email library (e.g. MailKit).
        await Task.CompletedTask;

        _logger.LogDebug("Email sent to {Recipient}", to);
    }

    public async Task SendOrderConfirmationAsync(int orderId, string recipientEmail, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending order confirmation for order {OrderId} to {Recipient}", orderId, recipientEmail);
        await SendEmailAsync(
            recipientEmail,
            $"Order #{orderId} Confirmation",
            $"Your order #{orderId} has been confirmed.",
            cancellationToken);
    }

    public async Task SendPasswordResetAsync(string email, string resetToken, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Sending password reset email to {Recipient}", email);
        await SendEmailAsync(
            email,
            "Password Reset Request",
            $"Use this token to reset your password: {resetToken}",
            cancellationToken);
    }
}
