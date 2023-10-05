using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VebTech.API.Controllers;

public class UserController : BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<User> AddUser([FromBody] AddUser.Request request, CancellationToken cancellationToken) =>
        await _mediator.Send(new AddUser.Request
        {
            Email = request.Email,
            Name = request.Name,
            Age = request.Age
        }, cancellationToken);
}