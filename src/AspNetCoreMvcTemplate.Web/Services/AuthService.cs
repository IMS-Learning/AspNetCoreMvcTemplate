using AspNetCoreMvcTemplate.Web.Helpers.Utilities;
using AspNetCoreMvcTemplate.Web.Models.Domain;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;
using AspNetCoreMvcTemplate.Web.Services.Interfaces;

namespace AspNetCoreMvcTemplate.Web.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IUserService userService, ILogger<AuthService> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public async Task<User?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Authenticating user {Username}", request.Username);

        var users = await _userService.GetUsersAsync(cancellationToken: cancellationToken);
        var user = users.FirstOrDefault(u =>
            u.Username == request.Username && u.IsActive);

        if (user == null)
        {
            _logger.LogWarning("Authentication failed for {Username}: user not found", request.Username);
            return null;
        }

        if (!CryptoHelper.VerifyPassword(request.Password, user.PasswordHash))
        {
            _logger.LogWarning("Authentication failed for {Username}: invalid password", request.Username);
            return null;
        }

        _logger.LogInformation("User {Username} authenticated successfully", request.Username);
        return user;
    }

    public async Task<bool> IsUsernameAvailableAsync(string username, CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetUsersAsync(cancellationToken: cancellationToken);
        return !users.Any(u => u.Username == username);
    }

    public async Task<bool> IsEmailAvailableAsync(string email, CancellationToken cancellationToken = default)
    {
        var users = await _userService.GetUsersAsync(cancellationToken: cancellationToken);
        return !users.Any(u => u.Email == email);
    }
}
