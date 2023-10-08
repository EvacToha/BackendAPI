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

            if (request.Modifiers.Filter.NameFilter != null)
                query = query.FilterByName(request.Modifiers.Filter.NameFilter);

            if (request.Modifiers.Filter.AgeFilter != null)
                query = query.FilterByAge(request.Modifiers.Filter.AgeFilter);
            
            if (request.Modifiers.Filter.EmailFilter != null)
            {
                query = query.FilterByEmail(request.Modifiers.Filter.EmailFilter);
            }
            if (request.Modifiers.Filter.RoleFilter != null)
            {
                query = query.FilterByRole(request.Modifiers.Filter.RoleFilter);
            }

            foreach (var sortingAction in request.Modifiers.Sorting.SortingActions)
            {
                if (sortingAction.IsAscending)
                {
                    query = query.OrderBy(Sorting.GetSortProperty(sortingAction.AttributeName));
                }
                else
                {
                    query = query.OrderByDescending(Sorting.GetSortProperty(sortingAction.AttributeName));
                }
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

