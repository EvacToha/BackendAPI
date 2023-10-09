using FluentValidation;
using MediatR;
using VebTech.Domain.Services;
using UserModel = VebTech.Domain.Models.Entities.User;

namespace VebTech.Application.Requests.User;

public class GetUser
{
    public class Request : IRequest<UserModel>
    {
        public long UserId { get; set; }
    }

    public class GetUserHandler : IRequestHandler<Request, UserModel>, IPipelineBehavior<Request, UserModel>
    {
        private readonly IUserService _userService;
        private readonly IValidator<Request> _validator;

        public GetUserHandler(IUserService userService, IValidator<Request> validator)
        {
            _userService = userService;
            _validator = validator;
        }
        
        public async Task<UserModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserById(request.UserId, cancellationToken);
            return user;
        }

        public async Task<UserModel> Handle(
            Request request, 
            RequestHandlerDelegate<UserModel> next, 
            CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            return await next();
        }
    }
}