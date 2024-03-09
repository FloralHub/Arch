namespace FloralHub.Arch.Api.Endpoints;

public static class BaseEndpoints
{
    public static RouteGroupBuilder MapBaseApi(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.GetHello();
        groupBuilder.GetGuid();
        groupBuilder.GetSchema();
        groupBuilder.AddSchema();

        return groupBuilder;
    }

    public static void GetHello(this IEndpointRouteBuilder endpointBuilder) =>
        endpointBuilder
            .MapGet(
                "/hello",
                static () => "Hello FloralHub.Arch!")
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

    public static void GetSchema(this IEndpointRouteBuilder endpointBuilder) =>
        endpointBuilder
            .MapGet("/schema/{schemaId}", (Guid schemaId) => GetSchema(schemaId))
            .WithName("GetSchema")
            .WithTags("Private")
            .WithSummary("Summary")
            .WithDisplayName("Display name")
            .WithOpenApi()
            .WithDescription("Description");

    public static void AddSchema(this IEndpointRouteBuilder endpointBuilder) =>
        endpointBuilder
            .MapPost("/schema/add", (Request request) => AddSchema(request))
            .WithName("AddSchema")
            .WithTags("Private")
            .WithSummary("Summary")
            .WithDisplayName("Display name")
            .WithOpenApi()
            .WithDescription("Description");

    private static object GetSchema(Guid schemaId) =>
        new
        {
            Id = schemaId,
            Name = "Schema name",
            Type = SchemaType.OneType,
            AnotherType = SchemaType.TwoType
        };

    private static object AddSchema(Request request) =>
        new
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Type = request.SchemaType,
            AnotherType = SchemaType.TwoType
        };
}
