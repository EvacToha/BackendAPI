using VebTech.Domain.Models.Entities;

namespace VebTech.Domain.Services;

public interface IUserService
{
    public Task<User> AddUser(User user, CancellationToken cancellationToken);
    public Task<User?> GetUserById(long userId, CancellationToken cancellationToken);
    public IQueryable<User> GetUsers();

    public Task<User> UpdateUser(User user, CancellationToken cancellationToken);
    public Task<int> RemoveUserById(long userId, CancellationToken cancellationToken);
}