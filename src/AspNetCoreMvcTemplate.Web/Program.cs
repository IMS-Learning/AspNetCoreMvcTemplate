using Microsoft.AspNetCore.Authentication.Cookies;
using AspNetCoreMvcTemplate.Web.Clients;
using AspNetCoreMvcTemplate.Web.Clients.Interfaces;
using AspNetCoreMvcTemplate.Web.Filters;
using AspNetCoreMvcTemplate.Web.Middleware;
using AspNetCoreMvcTemplate.Web.Models.Options;
using AspNetCoreMvcTemplate.Web.Services;
using AspNetCoreMvcTemplate.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// Configuration
// ============================================================
builder.Services
    .Configure<AuthOptions>(builder.Configuration.GetSection("Auth"))
    .Configure<DatabaseOptions>(builder.Configuration.GetSection("Database"))
    .Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"))
    .Configure<ExternalApiOptions>(builder.Configuration.GetSection("ExternalApi"));

// ============================================================
// MVC & Filters
// ============================================================
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

// ============================================================
// Authentication & Authorization
// ============================================================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// ============================================================
// External API Clients (HttpClients)
// ============================================================
builder.Services.AddHttpClient<IExternalApiClient, ExternalApiClient>(client =>
{
    var baseUrl = builder.Configuration["ExternalApi:BaseUrl"];
    if (!string.IsNullOrEmpty(baseUrl))
        client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<IPaymentGatewayClient, PaymentGatewayClient>(client =>
{
    var baseUrl = builder.Configuration["PaymentGateway:BaseUrl"];
    if (!string.IsNullOrEmpty(baseUrl))
        client.BaseAddress = new Uri(baseUrl);
});

// ============================================================
// Business Services
// ============================================================
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// ============================================================
// Infrastructure
// ============================================================
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// ============================================================
// Middleware Pipeline (order matters)
// ============================================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<SecurityHeadersMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

