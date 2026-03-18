using AspNetCoreMvcTemplate.Web.Helpers.Utilities;
using AspNetCoreMvcTemplate.Web.Models.Domain;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;
using AspNetCoreMvcTemplate.Web.Services.Interfaces;

namespace AspNetCoreMvcTemplate.Web.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;

    // In a real application, inject a repository or DbContext here.
    private static readonly List<User> _users = [];
    private static int _nextId = 1;

    public UserService(ILogger<UserService> logger)
    {
        _logger = logger;
    }

    public async Task<User?> GetUserAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving user {UserId}", id);
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
            _logger.LogWarning("User {UserId} not found", id);
        return await Task.FromResult(user);
    }

    public async Task<IEnumerable<User>> GetUsersAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Retrieving users page {Page} with size {PageSize}", page, pageSize);
        var users = _users
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        return await Task.FromResult(users);
    }

    public async Task<int> GetUserCountAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_users.Count);
    }

    public async Task<User> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating user {Username}", request.Username);

        if (_users.Any(u => u.Username == request.Username))
            throw new InvalidOperationException($"Username '{request.Username}' is already taken.");

        if (_users.Any(u => u.Email == request.Email))
            throw new InvalidOperationException($"Email '{request.Email}' is already registered.");

        var user = new User
        {
            Id = _nextId++,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Username = request.Username,
            PasswordHash = CryptoHelper.HashPassword(request.Password),
            Role = "User",
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        _users.Add(user);
        _logger.LogInformation("User {UserId} created successfully", user.Id);
        return await Task.FromResult(user);
    }

    public async Task<User> UpdateUserAsync(int id, CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating user {UserId}", id);
        var user = _users.FirstOrDefault(u => u.Id == id)
            ?? throw new KeyNotFoundException($"User {id} not found.");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.UpdatedDate = DateTime.UtcNow;
        return await Task.FromResult(user);
    }

    public async Task DeleteUserAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting user {UserId}", id);
        var user = _users.FirstOrDefault(u => u.Id == id)
            ?? throw new KeyNotFoundException($"User {id} not found.");
        _users.Remove(user);
        await Task.CompletedTask;
    }
}
