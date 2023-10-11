using VebTech.Domain.Models.Entities;

namespace VebTech.Domain.Services;

public interface IUserService
{
    /// <summary>
    /// Добавлеяет пользователя в базу данных.
    /// </summary>
    /// <param name="user">Добавляемый пользователь.</param>
    /// <param name="roles">Применяемые роли к пользователю.</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>Возвращает объект User с добавленными ролями.</returns>
    public Task<User> AddUser(User user, IEnumerable<UserRole> roles, CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавляет пользователю с id равным userId передаваемую роль.
    /// </summary>
    /// <param name="userId">Id пользователя.</param>
    /// <param name="userRole">Добавляемая роль.</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>Возвращает объект User с добавленной ролью.</returns>
    public Task<User> AddUserRole(long userId, UserRole userRole, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получает пользователя из базы данных по id.
    /// </summary>
    /// <param name="userId">Id пользователя.</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>Возвращает объект User.</returns>
    public Task<User> GetUserById(long userId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Возращает пользователей из базы данных.
    /// </summary>
    /// <returns>Возвращает пользователей как интерфейс IQueryable.</returns>
    public IQueryable<User> GetUsersAsQueryable();
    
    /// <summary>
    /// Обновляет пользователя в базе данных согласно объекту User.
    /// </summary>
    /// <param name="user">Новый юзер.</param>
    /// <param name="roles">Применяемые роли.</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>Возвращает обновленный объект User с добавленными ролями.</returns>
    public Task<User> UpdateUser(User user, IEnumerable<UserRole> roles, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет пользователя из базы данных по его айди.
    /// </summary>
    /// <param name="userId">Айди пользователя.</param>
    /// <param name="cancellationToken">Токен.</param>
    public Task RemoveUserById(long userId, CancellationToken cancellationToken);
}