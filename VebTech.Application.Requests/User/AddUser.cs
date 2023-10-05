using System.Text.Json.Serialization;
using MediatR;
using VebTech.Domain.Services;
using UserModel = VebTech.Domain.Models.Entities.User;

namespace VebTech.Application.Requests.User;


public class AddUser
{
    public class Request : UserModel, IRequest<UserModel>
    {
        [JsonIgnore]
        public long UserId { get; set; }
    }

    public class GetUserHandler : IRequestHandler<Request, UserModel>, IPipelineBehavior<Request, UserModel>
    {
        private readonly IUserService _userService;
        
        public GetUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        
        public async Task<UserModel> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = new UserModel
            {
                Email = request.Email,
                Name = request.Name,
                Age = request.Age,
            };

            return await _userService.AddUser(user, cancellationToken);
        }

        public async Task<UserModel> Handle(
            Request request, 
            RequestHandlerDelegate<UserModel> next, 
            CancellationToken cancellationToken)
        {
            return await next();
        }
    }
}