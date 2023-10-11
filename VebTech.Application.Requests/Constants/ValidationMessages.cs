namespace VebTech.Application.Requests.Constants;

public static class ValidationMessages
{
    public const string NotEmptyValidator = "Поле не должно быть пустым.";
    
    public const string IncorrectFilterMethodValidator = "Атрибут не поддерживает указаный фильтр.";
    
    public const string NotUniqueEntityValidator = "Пользователь с таким Email уже существует..";
    
    public const string EntityDoesNotExistValidator = "Пользователя не существует.";

    public const string IncorrectAgeValidator = "Поле возраста должно быть положительным.";
    
    public const string IncorrectIdValidator = "Айди не должно быть меньше либо равно нулю.";

    public const string InvalidEmail = "Некорректный Email.";

    public static readonly string MaximumLengthValidator = $"Длина значения не должна превышать {ValidationConstants.StringMaximumLength} символов.";
}