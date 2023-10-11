using FluentValidation;
using VebTech.Application.Requests.Constants;
using VebTech.Application.Requests.User;
using VebTech.Domain.Services.ValidationServices;

namespace VebTech.Application.Requests.Validation;

public class AddUserRoleRequestValidator : AbstractValidator<AddUserRole.Request>
{
    public AddUserRoleRequestValidator(IValidationUserService validationUserService)
    {
        RuleFor(r => r.UserId)
            .NotEmpty()
            .WithMessage(ValidationMessages.NotEmptyValidator)
            .WithErrorCode("400")
            .GreaterThan(ValidationConstants.IdMinimumValue)
            .WithMessage(ValidationMessages.IncorrectIdValidator)
            .WithErrorCode("400")
            .MustAsync(validationUserService.IsExists)
            .WithMessage(ValidationMessages.EntityDoesNotExistValidator)
            .WithErrorCode("404");
        
        RuleFor(r => r)
            .MustAsync(async (r, token) => await validationUserService.IsRoleBelongToUser(r.UserRole, r.UserId, token))
            .WithMessage(ValidationMessages.NotUniqueRoleValidator)
            .WithErrorCode("400");
    }
}