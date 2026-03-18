using System.Diagnostics;

namespace AspNetCoreMvcTemplate.Web.Middleware;

public class PerformanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMiddleware> _logger;
    private const int SLOW_REQUEST_THRESHOLD_MS = 500;

    public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        if (stopwatch.ElapsedMilliseconds > SLOW_REQUEST_THRESHOLD_MS)
        {
            _logger.LogWarning(
                "Slow request detected: {Method} {Path} took {ElapsedMs}ms",
                context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds);
        }
    }
}
