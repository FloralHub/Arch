namespace AlchemyLub.Arch.Api.Endpoints;

public static class BaseEndpoints
{
    public static RouteGroupBuilder MapBaseApi(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.GetHello();
        groupBuilder.GetGuid();

        return groupBuilder;
    }

    public static void GetHello(this IEndpointRouteBuilder endpointBuilder) =>
        endpointBuilder
            .MapGet(
                "/hello",
                static () => "Hello AlchemyLub.Arch!")
            .WithName("HelloEndpoint")
            .WithTags("Public")
            .WithOpenApi()
            .WithDescription("Базовый endpoint");

    public static void GetGuid(this IEndpointRouteBuilder endpointBuilder) =>
        endpointBuilder
            .MapGet("/guid", static () => $"{Guid.NewGuid()}")
            .WithName("GuidEndpoint")
            .WithTags("Private")
            .WithOpenApi()
            .WithDescription("Приватный базовый endpoint");
}
