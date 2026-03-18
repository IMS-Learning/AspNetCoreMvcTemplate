# Development Guide

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### Running the Application

```bash
# Clone the repository
git clone <repository-url>
cd AspNetCoreMvcTemplate

# Restore dependencies
dotnet restore

# Run the application
dotnet run --project src/WebApp/WebApp.csproj
```

The application will be available at `https://localhost:7xxx` or `http://localhost:5xxx`.

### Building

```bash
dotnet build
```

### Configuration

Copy `appsettings.Development.json` to configure development settings. Never commit secrets to source control.

```json
{
  "Auth": {
    "JwtSecret": "your-secret-key-here"
  },
  "Database": {
    "ConnectionString": "Server=localhost;Database=WebApp;"
  },
  "Smtp": {
    "Host": "smtp.example.com",
    "Username": "user@example.com",
    "Password": "your-smtp-password",
    "FromAddress": "no-reply@example.com",
    "FromName": "WebApp"
  }
}
```

## Project Structure

See [ARCHITECTURE.md](ARCHITECTURE.md) for full architecture documentation.

## Adding a New Feature

1. **Domain Model**: Add model in `Models/Domain/`
2. **DTOs**: Add request/response DTOs in `Models/DTOs/`
3. **ViewModel**: Add view model in `Models/ViewModels/`
4. **Service Interface**: Define interface in `Services/Interfaces/`
5. **Service Implementation**: Implement in `Services/`
6. **Controller**: Add controller in `Controllers/`
7. **Views**: Add views in `Views/<Feature>/`
8. **Register**: Register service in `Program.cs`

## Code Style

- Follow C# naming conventions
- Use `async`/`await` for all I/O
- Inject dependencies via constructor
- Validate inputs with Data Annotations or FluentValidation
- Log important operations using `ILogger<T>`
