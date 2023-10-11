using VebTech.Domain.Models.Entities;

namespace VebTech.Domain.Services.ValidationServices;

public interface IValidationUserService
{
    /// <summary>
    /// Проверяет есть ли передаваемый email у имеющихся юзеров.
    /// </summary>
    /// <param name="email">Проверяемый email</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>True - если email уникальный. False - если у одного из пользователей имеется данный email.</returns>
    public Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken);
    
    /// <summary>
    /// Проверяет существует ли юзер с заданным айди.
    /// </summary>
    /// <param name="userId">Проверяемое айди.</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>True - если пользователь с айди userId существует. False - если пользователя не существует.</returns>
    public Task<bool> IsExists(long userId , CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет отношение email к юзеру.
    /// </summary>
    /// <param name="email">Проверяемый email.</param>
    /// <param name="userId">Проверяемое айди.</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>True - если у юзера с айди равным userId поле email равно проверяемому email. False - если не равно.</returns>
    public Task<bool> IsEmailBelongToUser(string email, long userId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Проверяет отношение роли к юзеру
    /// </summary>
    /// <param name="userRole">Проверяемая роль.</param>
    /// <param name="userId">Проверяемое айди.</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>True - если у юзера с айди равным userId имеется роль userRole. False - если не имеется.</returns>
    public Task<bool> IsRoleBelongToUser(UserRole userRole , long userId, CancellationToken cancellationToken);
}