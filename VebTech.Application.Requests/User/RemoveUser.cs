using FluentValidation;
using MediatR;
using VebTech.Domain.Services;

namespace VebTech.Application.Requests.User;

public class RemoveUser
{
    public class Request : IRequest
    {
        public long UserId { get; set; }
    }

    public class RemoveUserHandler : IRequestHandler<Request, Unit>, IPipelineBehavior<Request,Unit>
    {
        private readonly IUserService _userService;
        private readonly IValidator<Request> _validator;

        public RemoveUserHandler(IUserService userService, IValidator<Request> validator)
        { 
            _userService = userService;
            _validator = validator;
        }

        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            await _userService.RemoveUserById(request.UserId, cancellationToken);
            return default;
        }

        public async Task<Unit> Handle(
            Request request,
            RequestHandlerDelegate<Unit> next,
            CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            return await next();
        }
    }
}