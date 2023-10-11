using Property = VebTech.Domain.Models.Queries.Modifiers.Property;
using FilterMethod = VebTech.Domain.Models.Queries.FilterAction.FilterMethod;

namespace VebTech.Domain.Models.Queries.Validation;

/// <summary>
/// Класс валидации запросов
/// </summary>
public static class QuariesValidation
{
    private static readonly List<FilterMethod> StringMethodsList = new()
    {
        FilterMethod.End,
        FilterMethod.Start,
        FilterMethod.Equals,
        FilterMethod.Contains
    };
    
    private static readonly List<FilterMethod> IntegerMethodsList = new() 
    {
        FilterMethod.Less,
        FilterMethod.More,
        FilterMethod.Equals
    };
    
    private static readonly List<Property> StringPropertyList = new() 
    {
        Property.Name,
        Property.Email,
        Property.UserRole
    };
    
    private static readonly List<Property> IntegerPropertyList = new() 
    {
        Property.Age
    };
    
    /// <summary>
    /// Валидация метода фильтра и валидируемого свойства
    /// </summary>
    /// <param name="method"> Метод фильтра</param>
    /// <param name="property"> Валидируемое свойство</param>
    /// <returns></returns>
    public static bool FilterValidate(FilterMethod method, Property property)
    {
        var isStringProperty = StringPropertyList.Any(p => p == property);
        var isIntegerProperty = IntegerPropertyList.Any(p => p == property);
        var isStringMethod = StringMethodsList.Any(m => m == method);
        var isIntegerMethod = IntegerMethodsList.Any(m => m == method);
        
        return isStringProperty && isStringMethod || isIntegerProperty && isIntegerMethod;
    }
}