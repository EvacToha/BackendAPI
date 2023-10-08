using MediatR;
using Microsoft.AspNetCore.Mvc;
using VebTech.API.Controllers.Constants;
using VebTech.Application.Requests.User;
using VebTech.Domain.Models.Entities;
using VebTech.Domain.Models.Queries;
using VebTech.Infrastructure.Database;

namespace VebTech.API.Controllers;

public class UserController : BaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator, DatabaseContext dbContext)
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
    
    [HttpPost(RouteNames.Id)]
    public async Task<User> AddUserRole(
        [FromQuery] string roleName,
        [FromRoute] long userId,
        CancellationToken cancellationToken) =>
        await _mediator.Send(
            new AddUserRole.Request 
            {  
                UserId = userId,
                RoleName = roleName,
            }, cancellationToken);

    
    [HttpGet(RouteNames.Id)]
    public async Task<User> GetUser([FromRoute] long userId, CancellationToken cancellationToken) =>
        await _mediator.Send(
            new GetUser.Request
            {
                UserId = userId
            }, cancellationToken);
    
    [HttpPost("/api/users")]
    public async Task<PaginatedList<User>> GetUsers(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        [FromBody] Filter filter,
        CancellationToken cancellationToken) =>
        await _mediator.Send(
            new GetUsers.Request
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Filter = filter
            }, cancellationToken);

    [HttpPut(RouteNames.Id)]
    public async Task<User> UpdateUser([FromRoute] long userId, UpdateUser.Request request, CancellationToken cancellationToken) =>
        await _mediator.Send(
            new UpdateUser.Request
            {  
                QueryId = userId,
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