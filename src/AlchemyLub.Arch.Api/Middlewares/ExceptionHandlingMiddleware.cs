namespace AlchemyLub.Arch.Api.Middlewares;

/// <summary>
/// Промежуточное ПО для обработки ошибок сервиса.
/// </summary>
/// <param name="logger"><see cref="ILogger{ErrorHandleMiddleware}"/></param>
internal sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);

            if (context.Response.StatusCode is StatusCodes.Status404NotFound)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync<ExceptionResponse>(
                    new(
                        HttpStatusCode.NotFound,
                        "Запрошенный endpoint не существует"));
            }
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, "An unexpected error occurred.");

        ExceptionResponse response = exception switch
        {
            ApplicationException => new ExceptionResponse(HttpStatusCode.BadRequest, "Application exception occurred."),
            KeyNotFoundException => new ExceptionResponse(HttpStatusCode.NotFound, "The request key not found."),
            UnauthorizedAccessException => new ExceptionResponse(HttpStatusCode.Unauthorized, "Unauthorized."),
            _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}
