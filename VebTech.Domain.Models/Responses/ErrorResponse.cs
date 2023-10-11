namespace VebTech.Domain.Models.Responses;

public class ErrorResponse
{
    public string PropertyName { get; set; }
    public string ErrorMessage { get; set; }
    public string AttemptedValue { get; set; }
    public string CustomState { get; set; }
    public string Severity { get; set; }
    public string ErrorCode { get; set; }
    public FormattedMessagePlaceholderValues FormattedMessagePlaceholderValues { get; set; }

}

public class FormattedMessagePlaceholderValues
{
    public string PropertyName { get; set; }
    public string PropertyValue { get; set; }
}