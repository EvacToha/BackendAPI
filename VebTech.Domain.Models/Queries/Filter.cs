using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using VebTech.Domain.Models.Entities;
using Property = VebTech.Domain.Models.Queries.Modifiers.Property;

namespace VebTech.Domain.Models.Queries;

/// <summary>
/// Оболочка для фильтров
/// </summary>
public class Filter
{
    /// <summary>
    /// Массив фильтров
    /// </summary>
    public IEnumerable<FilterAction> FilterActions { get; set; }
}

/// <summary>
/// Фильтрующее действие
/// </summary>
public class FilterAction
{
    /// <summary>
    /// Перечисление фильтрующих методов
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FilterMethod 
    {
        Start,
        End,
        Contains,
        Equals,
        Less,
        More
    }
    
    public Property Property { get; set; }

    /// <summary>
    /// Фильтрующее значение
    /// </summary>
    public string FilterValue {get; set; }

    public FilterMethod Method {get; set; }
}

/// <summary>
/// Реализация фильтров
/// </summary>
public static class FilterQuery
{
    /// <summary>
    /// Фильтрация по имени
    /// </summary>
    /// <param name="query">Запрос из базы данных</param>
    /// <param name="filterAction"> Фильтрующее действие</param>
    /// <returns></returns>
    public static IQueryable<User> FilterByName(this IQueryable<User> query, FilterAction filterAction)
    {
        return filterAction.Method switch
        {
            FilterAction.FilterMethod.Start => query.Where(u => u.Name.StartsWith(filterAction.FilterValue)),
            FilterAction.FilterMethod.End => query.Where(u => u.Name.EndsWith(filterAction.FilterValue)),
            FilterAction.FilterMethod.Equals => query.Where(u => u.Name == filterAction.FilterValue),
            FilterAction.FilterMethod.Contains => query.Where(u => u.Name.Contains(filterAction.FilterValue)),
            _ => query.Where(u => u.Name == filterAction.FilterValue)
        };
    }
    
    /// <summary>
    /// Фильтрация по возрасту
    /// </summary>
    /// <param name="query">Запрос из базы данных</param>
    /// <param name="filterAction"> Фильтрующее действие</param>
    /// <returns></returns>
    public static IQueryable<User> FilterByEmail(this IQueryable<User> query, FilterAction filterAction)
    {
        return filterAction.Method switch
        {
            FilterAction.FilterMethod.Start => query.Where(u => u.Email.StartsWith(filterAction.FilterValue)),
            FilterAction.FilterMethod.End => query.Where(u => u.Email.EndsWith(filterAction.FilterValue)),
            FilterAction.FilterMethod.Equals => query.Where(u => u.Email == filterAction.FilterValue),
            FilterAction.FilterMethod.Contains => query.Where(u => u.Email.Contains(filterAction.FilterValue)),
            _ => query.Where(u => u.Email == filterAction.FilterValue)
        };
    }
    
    /// <summary>
    /// Фильтрация по возрасту
    /// </summary>
    /// <param name="query">Запрос из базы данных</param>
    /// <param name="filterAction"> Фильтрующее действие</param>
    /// <returns></returns>
    public static IQueryable<User> FilterByAge(this IQueryable<User> query, FilterAction filterAction)
    {
        var filterValue = int.Parse(filterAction.FilterValue);
        return filterAction.Method switch
        {
            FilterAction.FilterMethod.Less => query.Where(u => u.Age < filterValue),
            FilterAction.FilterMethod.More => query.Where(u => u.Age > filterValue),
            FilterAction.FilterMethod.Equals => query.Where(u => u.Age == filterValue),
            _ => query.Where(u => u.Age == filterValue)
        };
    }
    
    /// <summary>
    /// Фильтрация по роли
    /// </summary>
    /// <param name="query">Запрос из базы данных</param>
    /// <param name="filterAction"> Фильтрующее действие</param>
    /// <returns></returns>
    public static IQueryable<User> FilterByRole(this IQueryable<User> query, FilterAction filterAction)
    {
        return filterAction.Method switch
        {
            FilterAction.FilterMethod.Start => query.Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.UserRole.ToString().StartsWith(filterAction.FilterValue))),
            FilterAction.FilterMethod.End => query.Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.UserRole.ToString().EndsWith(filterAction.FilterValue))),
            FilterAction.FilterMethod.Equals => query.Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.UserRole.ToString() == filterAction.FilterValue)),
            FilterAction.FilterMethod.Contains => query.Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.UserRole.ToString().Contains(filterAction.FilterValue))),
            _ => query.Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.UserRole.ToString() == filterAction.FilterValue))
        };
    }
}