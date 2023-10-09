using FluentValidation;
using VebTech.Application.Requests.User;
using VebTech.Application.Requests.Constants;
using VebTech.Domain.Services.ValidationServices;

namespace VebTech.Application.Requests.Validation;

public class GetUserRequestValidator : AbstractValidator<GetUser.Request>
{
    public GetUserRequestValidator(IValidationUserService validationUserService)
    {
        RuleFor(r => r.UserId)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .GreaterThan(ValidationConstants.IdMinimumValue)
            .WithMessage(ValidationMessages.IncorrectIdValidator)
            .MustAsync(validationUserService.IsExists)
            .WithMessage(ValidationMessages.EntityDoesNotExistValidator);
    }
}