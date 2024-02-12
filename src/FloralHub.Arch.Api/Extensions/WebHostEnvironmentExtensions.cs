namespace FloralHub.Arch.Api.Extensions;

public static class WebHostEnvironmentExtensions
{
    public static bool IsTest(this IWebHostEnvironment environment) =>
        string.Equals(
            environment.EnvironmentName,
            Constants.TestEnvironment,
            StringComparison.OrdinalIgnoreCase);
}
