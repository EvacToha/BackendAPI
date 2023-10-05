namespace VebTech.Domain.Models.Queries;

public class PaginatedList<T>
{
    public IEnumerable<T> Data { get; set; }

    public int TotalCount { get; set; }

    public int PageNumber { get; set; }
}