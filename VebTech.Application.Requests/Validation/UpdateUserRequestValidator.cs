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
            .WithErrorCode("400")
            .GreaterThan(ValidationConstants.IdMinimumValue)
            .WithMessage(ValidationMessages.IncorrectIdValidator)
            .WithErrorCode("400")
            .MustAsync(validationUserService.IsExists)
            .WithMessage(ValidationMessages.EntityDoesNotExistValidator)
            .WithErrorCode("404");
        

        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .WithErrorCode("400")
            .MaximumLength(ValidationConstants.StringMaximumLength)
            .WithMessage(ValidationMessages.MaximumLengthValidator)
            .WithErrorCode("400")
            .EmailAddress()
            .WithMessage(ValidationMessages.InvalidEmail)
            .WithErrorCode("400");

        RuleFor(r => r.Email)
            .MustAsync(validationUserService.IsEmailUnique)
            .WithMessage(ValidationMessages.NotUniqueEntityValidator)
            .WhenAsync(async (r, token) => !await validationUserService.IsEmailBelongToUser(r.Email, r.QueryId, token))
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