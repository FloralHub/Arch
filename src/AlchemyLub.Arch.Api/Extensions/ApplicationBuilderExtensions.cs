namespace AlchemyLub.Arch.Api.Extensions;

/// <summary>
/// Методы расширения для <see cref="IApplicationBuilder"/>
/// </summary>
internal static class ApplicationBuilderExtensions
{
    internal static IApplicationBuilder UseErrorHandleMiddleware(this IApplicationBuilder app) =>
        app.UseMiddleware<ExceptionHandlingMiddleware>();

    public static bool Any<TSource>(this List<TSource> source) => source.Count > 0;
    public static bool Any<TSource>(this TSource[] source) => source.Length > 0;
}
