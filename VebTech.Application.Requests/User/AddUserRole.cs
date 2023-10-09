using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;
using VebTech.Domain.Models.Entities;
using VebTech.Domain.Services;
using UserModel = VebTech.Domain.Models.Entities.User;
namespace VebTech.Application.Requests.User;

public class AddUserRole
{
    public class Request : IRequest<UserModel>
    {
        public long UserId;
        public string RoleName;
    }

    public class AddUserRoleHandler : IRequestHandler<Request, UserModel>, IPipelineBehavior<Request, UserModel>
    {
        private readonly IUserService _userService;
        private readonly IValidator<Request> _validator;

        public AddUserRoleHandler(IUserService userService, IValidator<Request> validator)
        {
            _userService = userService;
            _validator = validator;
        }
        
        public async Task<UserModel> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _userService.AddRoleUser(request.UserId, request.RoleName, cancellationToken);
            
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