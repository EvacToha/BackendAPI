using FluentValidation;
using VebTech.Application.Requests.Constants;
using VebTech.Application.Requests.User;
using VebTech.Domain.Models.Queries;
using VebTech.Domain.Models.Queries.Validation;

namespace VebTech.Application.Requests.Validation;

public class GetUsersRequestValidator : AbstractValidator<GetUsers.Request>
{
    public GetUsersRequestValidator()
    {

        RuleForEach(r => r.Modifiers.Filter!.FilterActions)
            .SetValidator(new FilterValidator())
            .When(r => r.Modifiers.Filter != null);
    }
}

public class FilterValidator : AbstractValidator<FilterAction>
{
    public FilterValidator()
    {
        RuleFor(f => f)
            .Must( f  => QuariesValidation.FilterValidate(f.Method, f.Property))
            .WithMessage(ValidationMessages.IncorrectFilterMethodValidator)
            .WithErrorCode("400");
    }
}