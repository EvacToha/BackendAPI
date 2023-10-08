using System.Text.Json.Serialization;
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

        public AddUserRoleHandler(IUserService userService)
        {
            _userService = userService;
        }
        
        public async Task<UserModel> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _userService.AddRoleUser(request.UserId, request.RoleName, cancellationToken);
            
        }

        public async Task<UserModel> Handle(Request request, RequestHandlerDelegate<UserModel> next, CancellationToken cancellationToken)
        {
            return await next();
        }
    }
}