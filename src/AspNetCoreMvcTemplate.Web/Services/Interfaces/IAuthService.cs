using AspNetCoreMvcTemplate.Web.Models.Domain;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;

namespace AspNetCoreMvcTemplate.Web.Services.Interfaces;

public interface IAuthService
{
    Task<User?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<bool> IsUsernameAvailableAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> IsEmailAvailableAsync(string email, CancellationToken cancellationToken = default);
}
