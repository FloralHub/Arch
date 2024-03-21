namespace FloralHub.Arch.Api.Services.Abstractions;

public interface IUserService
{
    public Task<User> GetUser(Guid userId);
}
