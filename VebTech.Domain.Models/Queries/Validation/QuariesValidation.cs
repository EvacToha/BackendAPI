using Property = VebTech.Domain.Models.Queries.Modifiers.Property;
using FilterMethod = VebTech.Domain.Models.Queries.FilterAction.FilterMethod;

namespace VebTech.Domain.Models.Queries.Validation;


public static class QuariesValidation
{
    private static readonly List<FilterMethod> _stringMethodsList = new()
    {
        FilterMethod.End,
        FilterMethod.Start,
        FilterMethod.Equals,
        FilterMethod.Contains
    };
    
    private static readonly List<FilterMethod> _integerMethodsList = new() 
    {
        FilterMethod.Less,
        FilterMethod.More,
        FilterMethod.Equals
    };
    
    private static readonly List<Property> _stringPropertyList = new() 
    {
        Property.Name,
        Property.Email,
        Property.UserRole
    };
    
    private static readonly List<Property> _integerPropertyList = new() 
    {
        Property.Age
    };
    public static bool FilterValidate(FilterMethod method, Property property)
    {
        var isStringProperty = _stringPropertyList.Any(p => p == property);
        var isIntegerProperty = _integerPropertyList.Any(p => p == property);
        var isStringMethod = _stringMethodsList.Any(m => m == method);
        var isIntegerMethod = _integerMethodsList.Any(m => m == method);
        
        return isStringProperty && isStringMethod || isIntegerProperty && isIntegerMethod;
    }
}