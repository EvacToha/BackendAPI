using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;
using VebTech.Domain.Models.Entities;
using VebTech.Domain.Services;
using UserModel = VebTech.Domain.Models.Entities.User;

namespace VebTech.Application.Requests.User;

public class UpdateUser
{
    public class Request : UserModel, IRequest<UserModel>
    {
        [JsonIgnore]
        public long QueryId { get; set; }
        
        [JsonIgnore]
        public long UserId { get; set; }
        
        public IEnumerable<UserRole> Roles { get; set; }
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
            var user = new UserModel
            {
                UserId = request.QueryId,
                Name = request.Name,
                Email = request.Email,
                Age = request.Age
            }; 
            return await _userService.UpdateUser(user, request.Roles, cancellationToken);
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