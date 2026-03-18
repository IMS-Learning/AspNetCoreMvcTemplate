# Backend Standards

## Service Pattern

Services contain all business logic. They are thin wrappers around data access clients.

```csharp
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IProductRepository repository, ILogger<ProductService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Product> GetProductAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Retrieving product {ProductId}", id);
        var product = await _repository.GetByIdAsync(id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Product {id} not found.");
        return product;
    }
}
```

## Controller Pattern

Controllers are thin orchestrators:

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(CreateProductRequest request)
{
    if (!ModelState.IsValid)
        return View(request);

    await _productService.CreateProductAsync(request);
    return RedirectToAction(nameof(Index));
}
```

## Naming Conventions

| Element | Pattern | Example |
|---------|---------|---------|
| Interface | `I[Name]` | `IUserService` |
| Service | `[Name]Service` | `UserService` |
| Controller | `[Name]Controller` | `UserController` |
| ViewModel | `[Page]ViewModel` | `UserListViewModel` |
| DTO Request | `[Action][Name]Request` | `CreateUserRequest` |
| DTO Response | `[Name]Response` | `UserResponse` |
| Domain Entity | `[Name]` | `User`, `Product` |
| Middleware | `[Name]Middleware` | `RequestLoggingMiddleware` |
| Filter | `[Name]Filter` | `GlobalExceptionFilter` |

## Security

- Always use `[ValidateAntiForgeryToken]` on POST actions
- Use `[Authorize]` on protected controllers/actions
- Validate all inputs using Data Annotations
- Hash passwords with `CryptoHelper.HashPassword()`
- Never log sensitive data
