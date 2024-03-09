WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, true)));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });

    options.SwaggerDoc("v1", new()
    {
        Version = "v1",
        Title = $"{nameof(FloralHub.Arch)} API",
        Description = "Сервис предоставляющий API для получения архитектурных схем и их генерации",
        Contact = new()
        {
            Name = "Author",
            Url = new("https://github.com/VladislavRudakoff"),
            Email = "vladislav.rudakoff@gmail.com"
        },
        License = new()
        {
            Name = "License",
            Url = new("https://mit-license.org/")
        }
    });
});
builder.Services.AddHealthChecks();

WebApplication app = builder.Build();

app.UseSwagger(options =>
    options.PreSerializeFilters.Add((swagger, request) =>
        swagger.Servers = new List<OpenApiServer> { new() { Url = $"https://{request.Host}" } }));

app.UseSwaggerUI(ui =>
{
    ui.DocExpansion(DocExpansion.List);
    //ui.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post);
    ui.ShowCommonExtensions();
    ui.ShowExtensions();
});

app.UseHttpsRedirection();
app.UseHealthChecks("/health");

app.MapGroup("/").MapBaseApi();

app.Run();
