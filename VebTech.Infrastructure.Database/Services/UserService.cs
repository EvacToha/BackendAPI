using Microsoft.EntityFrameworkCore;
using VebTech.Domain.Models.Entities;
using VebTech.Domain.Services;

namespace VebTech.Infrastructure.Database.Services;

public class UserService : IUserService
{
    private readonly DatabaseContext _dbContext;

    public UserService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> AddUser(User user, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<User> AddRoleUser(long userId, string roleName, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
        if (role is null)
        {
            role = new Role { Name = roleName };
            await _dbContext.Roles.AddAsync(role, cancellationToken);
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
        
        user!.Roles.Add(role);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User?> GetUserById(long userId, CancellationToken cancellationToken) =>
        await _dbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
    


    public IQueryable<User> GetUsers() => 
        _dbContext.Users.AsQueryable();
    


    public async Task<User> UpdateUser(User user, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users.FirstAsync(it => it.UserId == user.UserId, cancellationToken);
        if(!user.Email.Equals(null))
            entity.Email = user.Email;
        if(!user.Name.Equals(null))
            entity.Name = user.Name;
        if(!user.Age.Equals(null))
            entity.Age = user.Age;

        entity.Roles = user.Roles;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<int> RemoveUserById(long userId, CancellationToken cancellationToken)
    {
        _dbContext.Users.Remove(new User { UserId = userId });
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}