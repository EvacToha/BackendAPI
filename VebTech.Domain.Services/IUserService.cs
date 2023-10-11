using VebTech.Domain.Models.Entities;

namespace VebTech.Domain.Services;

public interface IUserService
{
    public Task<User> AddUser(User user, IEnumerable<UserRole> roles, CancellationToken cancellationToken);
    public Task<User> AddRoleUser(long userId, UserRole userRole, CancellationToken cancellationToken);
    public Task<User> GetUserById(long userId, CancellationToken cancellationToken);
    public IQueryable<User> GetUsers();

    public Task<User> UpdateUser(User user, IEnumerable<UserRole> roles, CancellationToken cancellationToken);
    public Task RemoveUserById(long userId, CancellationToken cancellationToken);
}