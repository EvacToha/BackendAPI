using System.Text.Json.Serialization;

namespace VebTech.Domain.Models.Entities;

/// <summary>
/// Доступные роли
/// </summary>
public enum UserRole {
    User,
    Moderator,
    Admin,
    SuperAdmin
}

/// <summary>
/// Роль
/// </summary>
public class Role
{
    /// <summary>
    /// Айди
    /// </summary>
    public long RoleId { get; set; }
    
    /// <summary>
    /// Роль
    /// </summary>
    public UserRole UserRole { get; set; }

    /// <summary>
    /// Пользователи с этой ролью
    /// </summary>
    [JsonIgnore]
    public ICollection<User> Users { get; set; } = new List<User>();
}