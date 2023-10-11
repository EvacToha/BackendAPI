using System.Linq.Expressions;
using VebTech.Domain.Models.Entities;
using Property = VebTech.Domain.Models.Queries.Modifiers.Property;

namespace VebTech.Domain.Models.Queries;

/// <summary>
/// Оболочка для сортировок
/// </summary>
public class Sorting
{
    /// <summary>
    /// Массив сортировок
    /// </summary>
    public IEnumerable<SortingAction> SortingActions { get; set; }

    /// <summary>
    /// Выражение по свойству
    /// </summary>
    /// <param name="property"> Свойство </param>
    /// <returns></returns>
    public static Expression<Func<User, object>> GetSortProperty(Property property)
    {
        return property switch
        {
            Property.Name => user => user.Name,
            Property.Age => user => user.Age,
            Property.Email => user => user.Email,
            _ => user => user.UserId,
        };
    }
}

/// <summary>
/// Сортирующее действие
/// </summary>
public class SortingAction
{
    public Property Property { get; set; }
    /// <summary>
    /// Флаг по возрастанию
    /// </summary>
    public bool IsAscending { get; set; }
}