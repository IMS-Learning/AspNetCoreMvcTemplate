using AspNetCoreMvcTemplate.Web.Models.DTOs.Responses;

namespace AspNetCoreMvcTemplate.Web.Models.ViewModels;

public class UserListViewModel
{
    public IEnumerable<UserResponse> Users { get; set; } = [];
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}
