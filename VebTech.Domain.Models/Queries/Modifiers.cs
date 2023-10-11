using System.Text.Json.Serialization;

namespace VebTech.Domain.Models.Queries;

/// <summary>
/// Модификатор запроса
/// </summary>
public class Modifiers
{
    /// <summary>
    /// Перечисление названий свойств
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Property
    {
        Name,
        Email,
        Age,
        UserRole
    }
   
    public Sorting? Sorting { get; set; }
    public Filter? Filter { get; set; }
}