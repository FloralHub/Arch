namespace AlchemyLub.Arch.Api.Extensions;

/// <summary>
/// Методы расширения для <see cref="IServiceCollection"/>
/// </summary>
internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddMiddlewares(this IServiceCollection services) =>
        services.AddScoped<ExceptionHandlingMiddleware>();
}
