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
            .MaximumLength(ValidationConstants.StringMaximumLength)
            .WithMessage(ValidationMessages.MaximumLengthValidator)
            .EmailAddress()
            .WithMessage(ValidationMessages.InvalidEmail)
            .MustAsync(validationUserService.IsEmailUnique)
            .WithMessage(ValidationMessages.NotUniqueEntityValidator);

        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .MaximumLength(ValidationConstants.StringMaximumLength)
            .WithMessage(ValidationMessages.MaximumLengthValidator);
        
        RuleFor(r => (int)r.Age)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.IncorrectAgeValidator);
    }
}