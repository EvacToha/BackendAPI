using Microsoft.EntityFrameworkCore;
using VebTech.Domain.Models.Entities;
using VebTech.Domain.Services.ValidationServices;

namespace VebTech.Infrastructure.Database.Services.Validation;

public class ValidationUserService : IValidationUserService
{
    private readonly DatabaseContext _dbContext;

    public ValidationUserService(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken) =>
        !await _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);

    public async Task<bool> IsExists(long userId, CancellationToken cancellationToken) =>
        await _dbContext.Users.AnyAsync(u => u.UserId == userId, cancellationToken);
    
    public async Task<bool> IsEmailBelongToUser(string email, long userId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstAsync(u => u.UserId == userId, cancellationToken);

        return user.Email == email;
    }
    
    public async Task<bool> IsRoleBelongToUser(UserRole userRole, long userId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.Include(u => u.Roles).FirstAsync(u => u.UserId == userId, cancellationToken);

        return user.Roles.All(r => r.UserRole != userRole);
    }
}