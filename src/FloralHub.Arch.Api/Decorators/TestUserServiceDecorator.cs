namespace FloralHub.Arch.Api.Decorators;

public class TestUserServiceDecorator(IUserService userService) : IUserService
{
    /// <inheritdoc />
    public async Task<User> GetUser(Guid userId)
    {
        User user = await userService.GetUser(userId);

        return user;
    }
}
