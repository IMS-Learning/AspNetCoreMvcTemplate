using WebApp.Models.Domain;
using WebApp.Models.DTOs.Requests;

namespace WebApp.Services.Interfaces;

public interface IAuthService
{
    Task<User?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<bool> IsUsernameAvailableAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> IsEmailAvailableAsync(string email, CancellationToken cancellationToken = default);
}
