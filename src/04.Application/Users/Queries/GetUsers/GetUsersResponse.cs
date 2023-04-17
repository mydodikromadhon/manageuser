using Microsoft.AspNetCore.Identity;

namespace CRUD.ManagementUser.Application.Users.Queries.GetUsers;

public class GetUsersResponse
{
    public List<IdentityUser> IdentityUsers { get; set; } = new();
}
