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

        public Filter Filter { get; set; }
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

            if (request.Filter.NameFilter != null)
                query = query.FilterByName(request.Filter.NameFilter);

            if (request.Filter.AgeFilter != null)
                query = query.FilterByAge(request.Filter.AgeFilter);
            
            if (request.Filter.EmailFilter != null)
            {
                query = query.FilterByEmail(request.Filter.EmailFilter);
            }
            if (request.Filter.RoleFilter != null)
            {
                query = query.FilterByRole(request.Filter.RoleFilter);
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

