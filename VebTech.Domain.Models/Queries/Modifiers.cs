using System.Text.Json.Serialization;

namespace VebTech.Domain.Models.Queries;

public class Modifiers
{
    /// <summary>
    /// Название свойства
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