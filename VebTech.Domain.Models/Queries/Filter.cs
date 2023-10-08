using Microsoft.EntityFrameworkCore;
using VebTech.Domain.Models.Entities;

namespace VebTech.Domain.Models.Queries;



public class Filter
{
    public StringFilter? NameFilter { get; set; }
    public StringFilter? EmailFilter { get; set; }
    public IntegerFilter? AgeFilter { get; set; }
    public StringFilter? RoleFilter { get; set; }
}

public class BaseFilter
{
    public enum StringMethod 
    {
        Start = -1,
        Equals = 0,
        End = 1,
        Contains = 2,
    }
    
    public enum IntegerMethod 
    {
        Less = -1,
        Equals = 0,
        More = 1
    }
}

public class StringFilter : BaseFilter
{
    public StringMethod FilterMethod { get; set; }
    public string FilterValue { get; set; }
}

public class IntegerFilter : BaseFilter
{
    public IntegerMethod FilterMethod { get; set; }
    public long FilterValue { get; set; }
}

public static class FilterQuery
{
    public static IQueryable<User> FilterByName(this IQueryable<User> query, StringFilter stringFilter)
    {
        return stringFilter.FilterMethod switch
        {
            BaseFilter.StringMethod.Start => query.Where(u => u.Name.StartsWith(stringFilter.FilterValue)),
            BaseFilter.StringMethod.End => query.Where(u => u.Name.EndsWith(stringFilter.FilterValue)),
            BaseFilter.StringMethod.Equals => query.Where(u => u.Name == stringFilter.FilterValue),
            BaseFilter.StringMethod.Contains => query.Where(u => u.Name.Contains(stringFilter.FilterValue)),
            _ => query.Where(u => u.Name == stringFilter.FilterValue)
        };
    }
    
    public static IQueryable<User> FilterByEmail(this IQueryable<User> query, StringFilter stringFilter)
    {
        return stringFilter.FilterMethod switch
        {
            BaseFilter.StringMethod.Start => query.Where(u => u.Email.StartsWith(stringFilter.FilterValue)),
            BaseFilter.StringMethod.End => query.Where(u => u.Email.EndsWith(stringFilter.FilterValue)),
            BaseFilter.StringMethod.Equals => query.Where(u => u.Email == stringFilter.FilterValue),
            BaseFilter.StringMethod.Contains => query.Where(u => u.Email.Contains(stringFilter.FilterValue)),
            _ => query.Where(u => u.Email == stringFilter.FilterValue)
        };
    }
    
    public static IQueryable<User> FilterByAge(this IQueryable<User> query, IntegerFilter integerFilter)
    {
        return integerFilter.FilterMethod switch
        {
            BaseFilter.IntegerMethod.Less => query.Where(u => u.Age < integerFilter.FilterValue),
            BaseFilter.IntegerMethod.More => query.Where(u => u.Age > integerFilter.FilterValue),
            BaseFilter.IntegerMethod.Equals => query.Where(u => u.Age == integerFilter.FilterValue),
            _ => query.Where(u => u.Age == integerFilter.FilterValue)
        };
    }
    
    public static IQueryable<User> FilterByRole(this IQueryable<User> query, StringFilter stringFilter)
    {
        return stringFilter.FilterMethod switch
        {
            BaseFilter.StringMethod.Start => query.Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.Name.StartsWith(stringFilter.FilterValue))),
            BaseFilter.StringMethod.End => query.Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.Name.EndsWith(stringFilter.FilterValue))),
            BaseFilter.StringMethod.Equals => query.Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.Name == stringFilter.FilterValue)),
            BaseFilter.StringMethod.Contains => query.Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.Name.Contains(stringFilter.FilterValue))),
            _ => query.Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.Name == stringFilter.FilterValue))
        };
    }
    
}