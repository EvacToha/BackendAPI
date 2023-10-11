using FluentValidation;
using VebTech.Application.Requests.Constants;
using VebTech.Application.Requests.User;
using VebTech.Domain.Services.ValidationServices;

namespace VebTech.Application.Requests.Validation;

public class AddUserRequestValidator : AbstractValidator<AddUser.Request>
{
    public AddUserRequestValidator(IValidationUserService validationUserService)
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .WithErrorCode("400")
            .MaximumLength(ValidationConstants.StringMaximumLength)
            .WithMessage(ValidationMessages.MaximumLengthValidator)
            .WithErrorCode("400")
            .EmailAddress()
            .WithMessage(ValidationMessages.InvalidEmail)
            .WithErrorCode("400")
            .MustAsync(validationUserService.IsEmailUnique)
            .WithMessage(ValidationMessages.NotUniqueEntityValidator)
            .WithErrorCode("400");

        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .WithErrorCode("400")
            .MaximumLength(ValidationConstants.StringMaximumLength)
            .WithMessage(ValidationMessages.MaximumLengthValidator)
            .WithErrorCode("400");
        
        RuleFor(r => r.Age)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .WithErrorCode("400")
            .GreaterThan(ValidationConstants.AgeMinimumValue)
            .WithMessage(ValidationMessages.IncorrectAgeValidator)
            .WithErrorCode("400");
    }
}