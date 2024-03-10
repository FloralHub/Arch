using Microsoft.AspNetCore.Mvc;

namespace FloralHub.Arch.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArchController : Controller
{
    [HttpGet]
    public Result<Guid> Index()
    {
        throw new KeyNotFoundException();

        //return new Result<Guid>(Guid.NewGuid(), new("Some error"));
    }

    public record Result<T>(T Value, Errors Errors);

    public record Errors(string Text);
}
