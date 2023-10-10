using System.Linq.Expressions;
using VebTech.Domain.Models.Entities;
using Property = VebTech.Domain.Models.Queries.Modifiers.Property;

namespace VebTech.Domain.Models.Queries;

public class Sorting
{
    public IEnumerable<SortingAction> SortingActions { get; set; }

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

public class SortingAction
{
    public Property Property { get; set; }
    public bool IsAscending { get; set; }
}