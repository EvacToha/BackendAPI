using FluentValidation;
using MediatR;
using VebTech.Domain.Services;
using UserModel = VebTech.Domain.Models.Entities.User;

namespace VebTech.Application.Requests.User;

public class UpdateUser
{
    public class Request : UserModel, IRequest<UserModel>
    {
    }

    public class GetStudentHandler : IRequestHandler<Request, UserModel>, IPipelineBehavior<Request, UserModel>
    {
        private readonly IUserService _studentService;

        public GetStudentHandler(IUserService studentService)
        {
            _studentService = studentService;
        }

        public async Task<UserModel> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _studentService.UpdateUser(request, cancellationToken);
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