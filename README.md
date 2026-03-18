# AspNetCoreMvcTemplate

A reusable, best-practice starter template for organizing and structuring modern ASP.NET Core MVC applications following clean architecture principles.

## Overview

This template implements a **layered, feature-organized architecture** with:

- **Presentation Layer**: MVC Controllers and Razor views
- **Business Logic Layer**: Services that orchestrate domain workflows
- **Data Access Layer**: API clients for external integrations
- **Domain Layer**: Models, DTOs, and ViewModels
- **Cross-Cutting**: Middleware, filters, configuration, and utilities

## Quick Start

```bash
dotnet restore
dotnet run --project src/WebApp/WebApp.csproj
```

## Documentation

- [Architecture](docs/ARCHITECTURE.md) - Project structure and layer responsibilities
- [Development Guide](docs/DEVELOPMENT_GUIDE.md) - Getting started for developers
- [Backend Standards](docs/BACKEND_STANDARDS.md) - Backend patterns and conventions
- [Frontend Standards](docs/FRONTEND_STANDARDS.md) - Frontend organization and patterns

## Tech Stack

- **Framework**: ASP.NET Core 8 MVC
- **Frontend**: Bootstrap 5, ES Modules
- **Authentication**: Cookie Authentication
- **Logging**: Microsoft.Extensions.Logging
