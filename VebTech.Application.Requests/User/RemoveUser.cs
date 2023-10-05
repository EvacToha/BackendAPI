using FluentValidation;
using MediatR;
using VebTech.Domain.Services;

namespace VebTech.Application.Requests.User;

public class RemoveUser
{
    public class Request : IRequest<int>
    {
        public long UserId { get; set; }
    }

    public class RemoveUserHandler : IRequestHandler<Request, int>, IPipelineBehavior<Request, int>
    {
        private readonly IUserService _userService;

        public RemoveUserHandler(IUserService userService)
        { 
            _userService = userService;
        }

        public async Task<int> Handle(Request request, CancellationToken cancellationToken)
        {
            var result = await _userService.RemoveUserById(request.UserId, cancellationToken);

            return result;
        }

        public async Task<int> Handle(
            Request request,
            RequestHandlerDelegate<int> next,
            CancellationToken cancellationToken)
        {   
            return await next();
        }
    }
}