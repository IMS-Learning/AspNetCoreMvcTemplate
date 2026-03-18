# Architecture Documentation

This file describes the architecture of the ASP.NET Core MVC web application template.

For the full architecture specification, see the problem statement in [../README.md](../README.md).

## Project Structure

```
src/AspNetCoreMvcTemplate.Web/
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
│   ├── css/                    # Stylesheets
│   │   ├── shared/             # Shared styles (header, footer, pagination)
│   │   ├── auth/               # Authentication-related styles
│   │   └── products/           # Product page styles
│   ├── js/                     # JavaScript files
│   │   ├── shared/             # Shared scripts
│   │   │   └── modules/        # JS modules
│   │   ├── auth/               # Authentication scripts
│   │   └── products/           # Product scripts
│   ├── images/                 # Image assets (logos, icons, etc.)
│   └── lib/                    # Third-party libraries (Bootstrap, jQuery, etc.)
│
├── Program.cs                  # Entry point & DI configuration
├── appsettings.json
└── AspNetCoreMvcTemplate.Web.csproj
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

## Frontend Architecture & CSS System

### Color Token System

The application uses a **CSS variable-based color token system** defined in `site.css`. This ensures a single source of truth for colors across all components.

#### Root Color Variables (`site.css`)
```css
:root {
  /* Primary Color - SLC.gov Blue */
  --bs-primary: #005695;
  --bs-primary-dark: #012040;
  --bs-primary-hover: #0052a3;
  --bs-primary-active: #004080;
  --bs-primary-light: #e6f2ff;
  
  /* Secondary Color - SLC.gov Gold */
  --bs-secondary: #F2A900;
  --bs-secondary-dark: #c28600;
  --bs-secondary-hover: #d99500;
  --bs-secondary-active: #b37d00;
  --bs-secondary-light: #fff4e6;
  
  /* Status Colors */
  --bs-success: #28a745;
  --bs-danger: #dc3545;
  --bs-warning: #ffc107;
  --bs-info: #17a2b8;
}
```

**Guidelines:**
- Update colors **only** in `site.css` `:root` block
- Use light variants (`--bs-primary-light`) for backgrounds/hover states
- Use hover/active variants for interaction states
- Status colors are reserved for semantic meaning (success, danger, warning, info)

### CSS Cascade Order

The stylesheets are loaded in strict order to ensure proper precedence:

1. **Bootstrap Framework** (via CDN/lib) - Base components and utilities
2. **`site.css`** - Global styles, layout utilities, and color variables (single source of truth)
3. **`bootstrap-overrides.css`** - Bootstrap component overrides using variables from site.css
4. **Component-specific CSS** - Feature-specific styling (card.css, header.css, footer.css, pagination.css)

**Location:** `Views/Shared/_Layout.cshtml`

### Component Styling

#### Card Wrapper Component (`card.css`)
All major views are wrapped in `.card-wrapper` for consistent presentation:
```html
<div class="card-wrapper">
    <!-- View content -->
</div>
```

**Styling includes:**
- Left border accent (primary color)
- Subtle shadow and rounded corners
- Padding consistency across views

#### Bootstrap Component Overrides (`bootstrap-overrides.css`)
All Bootstrap components are customized to use the color system:

- **Buttons** (.btn-primary, .btn-secondary, .btn-outline-*)
- **Links** (a, a:hover)
- **Navigation** (.navbar-dark with primary color)
- **Forms** (.form-control, .form-select, .form-check-input focus states)
- **Alerts** (.alert-primary uses light background with dark text)
- **Badges** (.badge.bg-primary, .badge.bg-secondary)
- **Pagination** (.page-link, .page-item.active)
- **Cards** (.card, .card-header use light backgrounds)
- **Dropdowns** (.dropdown-item hover states)
- **Tooltips & Popovers** (backgrounds use primary color)

**Key principle:** All overrides reference CSS variables, not hardcoded values. This ensures consistency across the UI and makes global color changes trivial.

### Shared Component Styles

Additional CSS files for common UI patterns:

- **`header.css`** - Navigation and header styling
- **`footer.css`** - Footer and footer-specific components
- **`pagination.css`** - Pagination component styling

### Adding New Styles

When adding new component styles:

1. Create a new CSS file in `wwwroot/css/shared/` or appropriate subdirectory
2. Use CSS variables from `site.css` (e.g., `var(--bs-primary)`)
3. Never hardcode colors
4. Add reference to `_Layout.cshtml` in proper cascade order
5. Prefer CSS classes over inline styles

### Images and Assets

Static images should be placed in `wwwroot/images/` and organized by type:
- Logos
- Icons
- Product images
- Background images

Reference images in CSS or HTML:
```html
<img src="/images/logo.png" alt="Logo" />
```

```css
background-image: url(/images/bg-pattern.png);
```
