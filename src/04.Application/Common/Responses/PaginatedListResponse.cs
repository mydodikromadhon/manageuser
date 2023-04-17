namespace CRUD.ManagementUser.Application.Common.Responses;

public class PaginatedListResponse<T>
{
    public IList<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
}
