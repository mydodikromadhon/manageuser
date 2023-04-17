using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CRUD.ManagementUser.Bsui.Pages.Users.Components;

public partial class DialogResetPassword
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private void ResetPassword()
    {
        MudDialog.Close(DialogResult.Ok(true));
    }
}
