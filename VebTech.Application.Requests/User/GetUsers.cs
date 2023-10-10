using MediatR;
using Microsoft.EntityFrameworkCore;
using VebTech.Domain.Models.Queries;
using VebTech.Domain.Services;
using UserModel = VebTech.Domain.Models.Entities.User;


namespace VebTech.Application.Requests.User;

public class GetUsers 
{
    public class Request : IRequest<PaginatedList<UserModel>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public Modifiers Modifiers { get; set; }
    }

    public class GetUsersHandler : IRequestHandler<Request, PaginatedList<UserModel>>,
        IPipelineBehavior<Request, PaginatedList<UserModel>>
    {
        private readonly IUserService _userService;
        
        
        public GetUsersHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<PaginatedList<UserModel>> Handle(Request request, CancellationToken cancellationToken)
        {
            var query = _userService.GetUsers();

            //применяем все фильтры
            if (request.Modifiers.Filter != null)
            {
                query = request.Modifiers.Filter.FilterActions.Aggregate(query, (current, sortingAction) => sortingAction.Property switch
                {
                    Modifiers.Property.Name => current.FilterByName(sortingAction),
                    Modifiers.Property.Age => current.FilterByAge(sortingAction),
                    Modifiers.Property.Email => current.FilterByEmail(sortingAction),
                    Modifiers.Property.UserRole => current.FilterByRole(sortingAction),
                    _ => current
                });
            }

            //применяем все сортировки
            if (request.Modifiers.Sorting != null)
            {
                query = request.Modifiers.Sorting.SortingActions.Aggregate(query, (current, sortingAction) => sortingAction.IsAscending switch
                {
                    true => current.OrderBy(Sorting.GetSortProperty(sortingAction.Property)),
                    _ => current.OrderByDescending(Sorting.GetSortProperty(sortingAction.Property))
                });
            }

            var totalCount = query.Count();
            
            if (request.PageNumber != default && request.PageSize != default)
            {
                query = query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize);
            }

            return new PaginatedList<UserModel>
            {
                Data = await query.ToListAsync(cancellationToken),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
            };
        }

        public async Task<PaginatedList<UserModel>> Handle(
            Request request, 
            RequestHandlerDelegate<PaginatedList<UserModel>> next, 
            CancellationToken cancellationToken)
        {
            return await next();
        }
    }
    
}

