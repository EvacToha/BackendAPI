using FluentValidation;
using VebTech.Application.Requests.Constants;
using VebTech.Application.Requests.User;
using VebTech.Domain.Services.ValidationServices;

namespace VebTech.Application.Requests.Validation;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUser.Request>
{
    public UpdateUserRequestValidator(IValidationUserService validationUserService)
    {
        RuleFor(r => r.QueryId)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .GreaterThan(ValidationConstants.IdMinimumValue)
            .WithMessage(ValidationMessages.IncorrectIdValidator)
            .MustAsync(validationUserService.IsExists)
            .WithMessage(ValidationMessages.EntityDoesNotExistValidator);

        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .MaximumLength(ValidationConstants.StringMaximumLength)
            .WithMessage(ValidationMessages.MaximumLengthValidator)
            .EmailAddress()
            .WithMessage(ValidationMessages.InvalidEmail);

        RuleFor(r => r.Email)
            .MustAsync(validationUserService.IsEmailUnique)
            .WithMessage(ValidationMessages.NotUniqueEntityValidator)
            .WhenAsync(async (r, token) => !await validationUserService.IsEmailBelongToUser(r.Email, r.QueryId, token));

        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .MaximumLength(ValidationConstants.StringMaximumLength)
            .WithMessage(ValidationMessages.MaximumLengthValidator);
        
        RuleFor(r => r.Age)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .GreaterThan(ValidationConstants.AgeMinimumValue)
            .WithMessage(ValidationMessages.IncorrectAgeValidator);
    }
}