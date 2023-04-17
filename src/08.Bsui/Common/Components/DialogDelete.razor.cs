using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CRUD.ManagementUser.Bsui.Common.Components;

public partial class DialogDelete
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private void Delete()
    {
        MudDialog.Close(DialogResult.Ok(true));
    }
}
