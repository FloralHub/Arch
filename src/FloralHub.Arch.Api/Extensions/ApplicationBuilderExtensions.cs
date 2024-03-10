namespace FloralHub.Arch.Api.Extensions;

/// <summary>
/// Методы расширения для <see cref="IApplicationBuilder"/>
/// </summary>
internal static class ApplicationBuilderExtensions
{
    internal static IApplicationBuilder UseErrorHandleMiddleware(this IApplicationBuilder app) =>
        app.UseMiddleware<ExceptionHandlingMiddleware>();
}
