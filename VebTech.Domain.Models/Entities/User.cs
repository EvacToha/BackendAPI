using System.Runtime.Serialization;

namespace VebTech.Domain.Models.Entities;

/// <summary>
/// Пользователь
/// </summary>
[DataContract(Name = "Пользователь")]
public class User
{
    /// <summary>
    /// Айди
    /// </summary>
    public long UserId { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Возраст
    /// </summary>
    public short Age { get; set; }
    
    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Роли пользователя
    /// </summary>
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}