using Microsoft.EntityFrameworkCore;
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
}