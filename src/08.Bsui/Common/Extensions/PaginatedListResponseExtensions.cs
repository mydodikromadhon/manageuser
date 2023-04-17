using MudBlazor;
using CRUD.ManagementUser.Application.Common.Responses;

namespace CRUD.ManagementUser.Bsui.Common.Extensions;

public static class PaginatedListResponseExtensions
{
    public static TableData<T> ToTableData<T>(this PaginatedListResponse<T> result)
    {
        return new TableData<T>() { TotalItems = result.TotalCount, Items = result.Items };
    }
}
