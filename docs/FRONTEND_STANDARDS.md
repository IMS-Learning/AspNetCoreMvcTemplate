# Frontend Standards

## JavaScript Module Organization

```
wwwroot/js/
├── site.js                    # Global entry point
├── shared/                    # Reusable utilities
│   ├── http-client.js         # API communication
│   ├── form-utils.js          # Form handling
│   ├── validation.js          # Client validation
│   ├── ui-helpers.js          # UI utilities
│   └── modules/               # Reusable UI components
│       ├── modal.js
│       ├── toast.js
│       └── loader.js
├── auth/
│   ├── login.js
│   └── register.js
└── products/
    ├── list.js
    └── detail.js
```

## CSS Organization

```
wwwroot/css/
├── site.css                   # Global styles & design tokens
├── shared/                    # Reused across features
│   ├── header.css
│   ├── footer.css
│   └── pagination.css
├── auth/
│   ├── login.css
│   └── register.css
└── products/
    ├── list.css
    └── detail.css
```

## HTML Standards

- **Always use semantic HTML** (`<header>`, `<nav>`, `<main>`, `<article>`, `<section>`, `<footer>`, `<button>`, etc.)
- **Always use Bootstrap** for styling and responsive design
- Use Bootstrap utility classes for spacing, alignment, and layout
- Ensure proper heading hierarchy (h1 > h2 > h3, etc.)
- Use ARIA attributes when semantic HTML alone is insufficient for accessibility

## Best Practices

- Use ES modules (`import`/`export`)
- Keep feature scripts in their own directory
- Place reusable code in `shared/`
- Avoid global state
- Validate on both client and server
- Use Bootstrap components for consistent UI (modals, forms, alerts, etc.)
- Write semantic, accessible HTML for better SEO and user experience
