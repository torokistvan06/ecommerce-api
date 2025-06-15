using System;
using EcommerceApi.Exceptions;

namespace EcommerceApi;

public class ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandler> _logger = logger;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext); // Continue the request pipeline
        }
        catch (NotFoundException ex)
        {
            _logger.LogError($"NotFoundException: {ex.Message}");
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsync($"Not Found: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled Exception: {ex.Message}");
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsync("An unexpected error occurred.");
        }
    }
}
