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
    public async Task<User> AddUser(User user, IEnumerable<UserRole> roles, CancellationToken cancellationToken)
    {
        foreach (var role in roles)
        {
            var entity = await _dbContext.Roles.FirstAsync(r => r.UserRole == role, cancellationToken);
            user.Roles.Add(entity);
        }
        await _dbContext.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<User> AddUserRole(long userId, UserRole userRole, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles.FirstAsync(r => r.UserRole == userRole, cancellationToken);
        var user = await _dbContext.Users.Include(u => u.Roles).FirstAsync(u => u.UserId == userId, cancellationToken);
        
        user.Roles.Add(role);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return user;
    }
    
    public async Task<User> GetUserById(long userId, CancellationToken cancellationToken) =>
        await _dbContext.Users.Include(u => u.Roles).FirstAsync(u => u.UserId == userId, cancellationToken);
    
    public IQueryable<User> GetUsersAsQueryable() => 
        _dbContext.Users.AsQueryable();
    
    public async Task<User> UpdateUser(User user, IEnumerable<UserRole> roles, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Users.Include(u => u.Roles).FirstAsync(it => it.UserId == user.UserId, cancellationToken);
        
        entity.Email = user.Email;
        entity.Name = user.Name;
        entity.Age = user.Age;
        entity.Roles = new List<Role>();
        
        var rolesList = roles.ToList();
        if (rolesList.Any())
        {
            foreach (var role in rolesList)
            {
                var entityRole = await _dbContext.Roles.FirstAsync(r => r.UserRole == role, cancellationToken);
                entity.Roles.Add(entityRole);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task RemoveUserById(long userId, CancellationToken cancellationToken)
    {
        _dbContext.Users.Remove(new User { UserId = userId });
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}