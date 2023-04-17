using CRUD.ManagementUser.Bsui.Common.Authorizations;
using CRUD.ManagementUser.Bsui.Common.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System.Security.Claims;

namespace CRUD.ManagementUser.Bsui.Pages.Roles.Components;

public partial class DialogAddPermission
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public IdentityRole Request { get; set; } = default!;

    public List<string> IdentityPermissions { get; set; } = new();

    public string SelectedPermission { get; set; } = default!;

    private bool _isLoading;

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    protected override void OnInitialized()
    {
        ReloadData();
    }

    private void ReloadData()
    {
        _isLoading = true;

        IdentityPermissions = Common.Authorizations.Permissions.All.ToList();

        _isLoading = false;
    }

    private async Task OnValidSubmit()
    {
        _isLoading = true;

        var response = await _roleManager.AddClaimAsync(Request, new Claim(AuthorizationClaimTypes.Permission, SelectedPermission));

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
