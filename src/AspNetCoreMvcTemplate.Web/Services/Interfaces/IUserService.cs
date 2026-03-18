using AspNetCoreMvcTemplate.Web.Models.Domain;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;

namespace AspNetCoreMvcTemplate.Web.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetUsersAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<int> GetUserCountAsync(CancellationToken cancellationToken = default);
    Task<User> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
    Task<User> UpdateUserAsync(int id, CreateUserRequest request, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(int id, CancellationToken cancellationToken = default);
}
