namespace CRUD.ManagementUser.Application.Services.CurrentUser;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string Username { get; }
    string IpAddress { get; }
}
