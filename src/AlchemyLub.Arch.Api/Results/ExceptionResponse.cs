namespace AlchemyLub.Arch.Api.Results;

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
