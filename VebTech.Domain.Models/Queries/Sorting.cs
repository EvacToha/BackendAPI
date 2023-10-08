using System.Linq.Expressions;
using VebTech.Domain.Models.Entities;

namespace VebTech.Domain.Models.Queries;

public class Sorting
{
    public IEnumerable<SortingAction> SortingActions { get; set; }

    public static Expression<Func<User, object>> GetSortProperty(string property)
    {
        return property.ToLower() switch
        {
            "name" => user => user.Name,
            "age" => user => user.Age,
            "email" => user => user.Email,
            _ => user => user.UserId,
        };
    }
}

public class SortingAction
{
    public string AttributeName { get; set; }
    public bool IsAscending { get; set; }
}