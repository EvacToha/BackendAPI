using MediatR;
using Microsoft.AspNetCore.Mvc;
using VebTech.API.Controllers.Constants;
using VebTech.Application.Requests.User;
using VebTech.Domain.Models.Entities;
using VebTech.Domain.Models.Queries;

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
        await _mediator.Send(
            new AddUser.Request 
            { 
                Email = request.Email, 
                Name = request.Name, 
                Age = request.Age 
            }, cancellationToken);
    
    [HttpGet(RouteNames.Id)]
    public async Task<User> GetUser([FromRoute] long userId, CancellationToken cancellationToken) =>
        await _mediator.Send(
            new GetUser.Request
            {
                UserId = userId
            }, cancellationToken);
    
    [HttpGet]
    public async Task<PaginatedList<User>> GetUsers(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken) =>
        await _mediator.Send(
            new GetUsers.Request
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            }, cancellationToken);

    [HttpPut]
    public async Task<User> UpdateUser(UpdateUser.Request request, CancellationToken cancellationToken) =>
        await _mediator.Send(
            new UpdateUser.Request
            {
                UserId = request.UserId,
                Age = request.Age,
                Email = request.Email,
                Roles = request.Roles
            }, cancellationToken);

    [HttpDelete(RouteNames.Id)]
    public async Task<int> RemoveUser([FromRoute] long userId, CancellationToken cancellationToken) =>
        await _mediator.Send(
            new RemoveUser.Request 
            { 
                UserId = userId, 
            }, cancellationToken);
}