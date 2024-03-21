namespace FloralHub.Arch.Api.Services;

public class UserService : IUserService
{
    /// <inheritdoc />
    public Task<User> GetUser(Guid userId)
    {
        Guid id = userId;

        return Task.FromResult(new User(id, "Simple User"));
    }
}
