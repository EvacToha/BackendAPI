namespace VebTech.Domain.Models.Queries;

/// <summary>
/// Лист для пагинации
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginatedList<T>
{
    public IEnumerable<T> Data { get; set; }

    /// <summary>
    /// Количество объектов
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Текущий лист
    /// </summary>
    public int PageNumber { get; set; }
}