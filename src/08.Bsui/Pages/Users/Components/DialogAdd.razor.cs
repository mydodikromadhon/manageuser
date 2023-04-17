using CRUD.ManagementUser.Bsui.Common.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using MudBlazor;

namespace CRUD.ManagementUser.Bsui.Pages.Users.Components;

public partial class DialogAdd
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public IdentityUser Request { get; set; } = default!;

    public string Password { get; set; } = default!;

    private bool _isLoading;

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private async Task OnValidSubmit()
    {
        _isLoading = true;

        Request.UserName = Request.Email;

        var response = await _userManager.CreateAsync(Request, Password);

        _isLoading = false;

        if (response.Errors.Any())
        {
            _snackbar.AddErrors(response.Errors.Select(x => x.Description));

            return;
        }

        MudDialog.Close(DialogResult.Ok(true));
    }

    private void OnInvalidSubmit(EditContext editContext)
    {
        _snackbar.AddErrors(editContext.GetValidationMessages());
    }
}
