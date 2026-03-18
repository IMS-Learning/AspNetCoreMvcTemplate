using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AspNetCoreMvcTemplate.Web.Models.ViewModels;
using AspNetCoreMvcTemplate.Web.Models.DTOs.Requests;
using AspNetCoreMvcTemplate.Web.Services.Interfaces;

namespace AspNetCoreMvcTemplate.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IAuthService authService,
        IUserService userService,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var request = new LoginRequest
        {
            Username = viewModel.Username,
            Password = viewModel.Password,
            RememberMe = viewModel.RememberMe
        };

        var user = await _authService.AuthenticateAsync(request);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(viewModel);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { IsPersistent = viewModel.RememberMe });

        _logger.LogInformation("User {Username} logged in", user.Username);

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new CreateUserViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(CreateUserViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        if (!await _authService.IsUsernameAvailableAsync(viewModel.Username))
        {
            ModelState.AddModelError(nameof(viewModel.Username), "This username is already taken.");
            return View(viewModel);
        }

        if (!await _authService.IsEmailAvailableAsync(viewModel.Email))
        {
            ModelState.AddModelError(nameof(viewModel.Email), "This email is already registered.");
            return View(viewModel);
        }

        try
        {
            var request = new CreateUserRequest
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Username = viewModel.Username,
                Password = viewModel.Password
            };

            await _userService.CreateUserAsync(request);
            TempData["SuccessMessage"] = "Registration successful. Please log in.";
            return RedirectToAction(nameof(Login));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(viewModel);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        _logger.LogInformation("User {Username} logged out", User.Identity?.Name);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }
}
