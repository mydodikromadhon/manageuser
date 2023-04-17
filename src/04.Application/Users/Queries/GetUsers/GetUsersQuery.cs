using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CRUD.ManagementUser.Application.Users.Queries.GetUsers;

public class GetUsersQuery : IRequest<GetUsersResponse>
{
}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
{
    private readonly UserManager<IdentityUser> _userManager;

	public GetUsersQueryHandler(UserManager<IdentityUser> userManager)
	{
        _userManager = userManager;
    }

    public async Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var response = new GetUsersResponse();

        response.IdentityUsers = await Task.FromResult(_userManager.Users.ToList());

        return response;
    }
}
