namespace CRUD.ManagementUser.Application.Services.Authorization.Models.GetAuthorizationInfo;

public class GetAuthorizationInfoResponse
{
    public IList<GetAuthorizationInfoRole> Roles { get; set; } = new List<GetAuthorizationInfoRole>();
}
