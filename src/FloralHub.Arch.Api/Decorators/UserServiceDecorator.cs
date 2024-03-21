namespace FloralHub.Arch.Api.Decorators;

public class UserServiceDecorator(ILogger<UserServiceDecorator> logger, IUserService inner) : IUserService
{
    /// <inheritdoc />
    public async Task<User> GetUser(Guid userId)
    {
        logger.LogWarning("Тут начинается опасность");

        User user = await inner.GetUser(userId);

        logger.LogWarning("Тут заканчивается опасность");

        return user;
    }
}
