using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers.Constants;
using WebApp.Helpers.Mappers;
using WebApp.Models.DTOs.Requests;
using WebApp.Models.DTOs.Responses;
using WebApp.Models.ViewModels;
using WebApp.Services.Interfaces;

namespace WebApp.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = AppConfig.DEFAULT_PAGE_SIZE)
    {
        var users = await _userService.GetUsersAsync(page, pageSize);
        var totalCount = await _userService.GetUserCountAsync();

        var viewModel = new UserListViewModel
        {
            Users = users.Select(EntityToDtoMapper.ToResponse),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var user = await _userService.GetUserAsync(id);
        if (user == null)
            return NotFound();

        return View(EntityToDtoMapper.ToResponse(user));
    }

    public IActionResult Create()
    {
        return View(new CreateUserViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        try
        {
            var request = DtoToEntityMapper.ToRequest(viewModel);
            await _userService.CreateUserAsync(request);
            TempData["SuccessMessage"] = "User created successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var user = await _userService.GetUserAsync(id);
        if (user == null)
            return NotFound();

        var viewModel = new CreateUserViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Username = user.Username
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CreateUserViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        try
        {
            var request = DtoToEntityMapper.ToRequest(viewModel);
            await _userService.UpdateUserAsync(id, request);
            TempData["SuccessMessage"] = "User updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userService.GetUserAsync(id);
        if (user == null)
            return NotFound();

        return View(EntityToDtoMapper.ToResponse(user));
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            TempData["SuccessMessage"] = "User deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
