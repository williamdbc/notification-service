using Newtonsoft.Json;
using NotificationService.Exceptions;

namespace NotificationService.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var error = new ExceptionResponse
        {
            Message = exception.Message
        };

        LogLevel logLevel;
        
        (error.Status, logLevel) = exception switch {
            NotFoundException => (StatusCodes.Status404NotFound, LogLevel.Warning),
            BusinessException => (StatusCodes.Status400BadRequest, LogLevel.Warning),
            _ => (StatusCodes.Status500InternalServerError, LogLevel.Error)
        };
            
        context.Response.StatusCode = error.Status;
        
        _logger.Log(logLevel, exception, exception.Message);
            
        var formattedJson = JsonConvert.SerializeObject(error);
        return context.Response.WriteAsync(formattedJson);
    }
    
}