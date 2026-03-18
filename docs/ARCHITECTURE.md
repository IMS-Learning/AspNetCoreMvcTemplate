# Architecture Documentation

This file describes the architecture of the ASP.NET Core MVC web application template.

For the full architecture specification, see the problem statement in [../README.md](../README.md).

## Project Structure

```
src/WebApp/
├── Clients/                    # External API integration (HttpClients)
│   ├── Interfaces/
│   │   ├── IExternalApiClient.cs
│   │   └── IPaymentGatewayClient.cs
│   ├── ExternalApiClient.cs
│   └── PaymentGatewayClient.cs
│
├── Controllers/                # MVC Controllers (thin orchestrators)
│   ├── HomeController.cs
│   ├── AuthController.cs
│   ├── UserController.cs
│   └── ProductController.cs
│
├── Filters/                    # Action/Exception filters
│   ├── GlobalExceptionFilter.cs
│   ├── AuthorizationFilter.cs
│   └── ValidateModelFilter.cs
│
├── Helpers/                    # Utilities organized by type
│   ├── Constants/
│   ├── Extensions/
│   ├── Factories/
│   ├── Mappers/
│   ├── Utilities/
│   └── Validators/
│
├── Logging/                    # Logging configuration
│   └── AppLoggerFactory.cs
│
├── Middleware/                 # Custom middleware
│   ├── ErrorHandlingMiddleware.cs
│   ├── SecurityHeadersMiddleware.cs
│   ├── RequestLoggingMiddleware.cs
│   └── PerformanceMiddleware.cs
│
├── Models/                     # Data models
│   ├── Domain/                 # Business entities
│   ├── DTOs/
│   │   ├── Requests/
│   │   └── Responses/
│   ├── ViewModels/
│   └── Options/
│
├── Services/                   # Business logic
│   ├── Interfaces/
│   ├── AuthService.cs
│   ├── UserService.cs
│   ├── ProductService.cs
│   ├── OrderService.cs
│   ├── EmailService.cs
│   └── PaymentService.cs
│
├── Views/                      # Razor templates
│   ├── Shared/
│   ├── Home/
│   ├── Auth/
│   ├── User/
│   └── Product/
│
├── wwwroot/                    # Static assets
│   ├── css/
│   │   ├── shared/
│   │   ├── auth/
│   │   └── products/
│   └── js/
│       ├── shared/
│       │   └── modules/
│       ├── auth/
│       └── products/
│
├── Program.cs                  # Entry point & DI configuration
├── appsettings.json
└── WebApp.csproj
```

## Layer Responsibilities

### Presentation Layer (Controllers)
- Handle HTTP requests/responses
- Validate ModelState
- Call services to process requests
- Select views/responses
- **Never** contain business logic

### Business Logic Layer (Services)
- Implement business workflows
- Orchestrate data access
- Validate business rules
- Handle application errors

### Data Access Layer (Clients)
- Encapsulate external API/database access
- Provide typed methods for API calls
- Handle serialization/deserialization

### Domain Layer (Models)
- Represent core business concepts
- Domain models, DTOs, ViewModels, Options
