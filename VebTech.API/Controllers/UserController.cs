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
    
    /// <summary>
    /// Добавить пользователя
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /user
    ///     {
    ///        "name" : "Anton",
    ///        "age" : 11,
    ///        "email": "abc@mail.ru",
    ///        "roles": [
    ///            "User", "SuperAdmin", "Admin" 
    ///        ]
    ///     }
    /// 
    /// </remarks>
    /// <param name="request">Пользователь</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<User> AddUser([FromBody] AddUser.Request request, CancellationToken cancellationToken) =>
        await _mediator.Send(
            new AddUser.Request 
            { 
                Email = request.Email, 
                Name = request.Name, 
                Age = request.Age,
                Roles = request.Roles
            }, cancellationToken);
    
    
    /// <summary>                                            
    /// Добавить роль пользователю                              
    /// </summary>                                           
    /// <param name="userRole">Добавляемая роль</param>
    /// <param name="userId">Айди пользователя</param> 
    /// <returns></returns>                                  
    [HttpPost(RouteNames.Id)]
    public async Task<User> AddUserRole(
        [FromQuery] UserRole userRole,
        [FromRoute] long userId,
        CancellationToken cancellationToken) =>
        await _mediator.Send(
            new AddUserRole.Request 
            {  
                UserId = userId,
                UserRole = userRole,
            }, cancellationToken);

    /// <summary>                                            
    /// Получить пользователя                              
    /// </summary>                                           
    /// <param name="userId">Айди пользователя</param> 
    /// <returns></returns>   
    [HttpGet(RouteNames.Id)]
    public async Task<User> GetUser([FromRoute] long userId, CancellationToken cancellationToken) =>
        await _mediator.Send(
            new GetUser.Request
            {
                UserId = userId
            }, cancellationToken);
    
    


    /// <summary>                                            
    /// Получить всех пользователей                              
    /// </summary>                                           
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="pageSize">Размер страницы</param> 
    /// <param name="modifiers">Модификаторы запроса</param>
    /// <remarks>
    ///
    /// Пример запроса:
    ///
    ///     POST /users
    ///     {
    ///       "sorting": {
    ///         "sortingActions": [
    ///           {
    ///             "attributeName": "name",
    ///             "isAscending": true
    ///           },
    ///           {
    ///             "attributeName": "age",
    ///             "isAscending": false
    ///           }
    ///         ]
    ///       },
    ///       "filter": {
    ///         "nameFilter": {
    ///           "filterMethod": 1,
    ///           "filterValue": "Vasya"
    ///         },
    ///         "emailFilter": {
    ///           "filterMethod": 1,
    ///           "filterValue": "@mail.ru"
    ///         },
    ///         "ageFilter": {
    ///           "filterMethod": 0,
    ///           "filterValue": 18
    ///         },
    ///          "roleFilter": {
    ///           "filterMethod": 0,
    ///           "filterValue": "admin"
    ///         }
    ///       }
    ///     }
    /// 
    /// </remarks>
    /// <returns></returns>  
    [HttpPost("/api/users")]
    public async Task<PaginatedList<User>> GetUsers(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        [FromBody] Modifiers modifiers,
        CancellationToken cancellationToken) =>
        await _mediator.Send(
            new GetUsers.Request
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Modifiers = modifiers
            }, cancellationToken);

    /// <summary>                                            
    /// Обновить пользователя                              
    /// </summary>                                           
    /// <param name="userId">Айди пользователя</param>
    /// <param name="request">Новый пользователь</param>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /user/1
    ///     {
    ///        "name" : "Anton",
    ///        "age" : 11,
    ///        "email": "abc@mail.ru",
    ///        "roles": [
    ///            "User", "SuperAdmin", "Admin" 
    ///        ]
    ///     }
    /// 
    /// </remarks>
    /// <returns></returns>  
    [HttpPut(RouteNames.Id)]
    public async Task<User> UpdateUser([FromRoute] long userId, [FromBody] UpdateUser.Request request, CancellationToken cancellationToken) =>
        await _mediator.Send(
            new UpdateUser.Request
            {  
                QueryId = userId,
                Age = request.Age,
                Email = request.Email,
                Name = request.Name,
                Roles = request.Roles
            }, cancellationToken);

    /// <summary>                                            
    /// Удалить пользователя                              
    /// </summary>                                           
    /// <param name="userId">Айди пользователя</param>
    /// <returns></returns>  
    [HttpDelete(RouteNames.Id)]
    public async Task<int> RemoveUser([FromRoute] long userId, CancellationToken cancellationToken) =>
        await _mediator.Send(
            new RemoveUser.Request 
            { 
                UserId = userId, 
            }, cancellationToken);
}