using CRUD.ManagementUser.Bsui.Common.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;

namespace CRUD.ManagementUser.Bsui.Pages.Users.Components;

public partial class DialogUserRoles
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    public IList<string> UserRoles { get; set; } = default!;

    [Parameter]
    public IdentityUser IdentityUser { get; set; } = default!;

    private bool _isLoading;

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    protected override async Task OnInitializedAsync()
    {
        await ReloadDataAsync();
    }

    private async Task ReloadDataAsync()
    {
        _isLoading = true;

        UserRoles = await _userManager.GetRolesAsync(IdentityUser);

        _isLoading = false;
    }

    private async Task DeleteUserRole(string roleName)
    {
        _isLoading = true;

        var response = await _userManager.RemoveFromRoleAsync(IdentityUser, roleName);

        _isLoading = false;

        if (response.Errors.Any())
        {
            _snackbar.AddErrors(response.Errors.Select(x => x.Description));

            return;
        }

        await ReloadDataAsync();
    }

    private async Task ShowDialogAddRole()
    {
        var parameters = new DialogParameters
        {
            { nameof(DialogAddRole.Request), IdentityUser }
        };

        var dialog = _dialogService.Show<DialogAddRole>("Add User to Role", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            _snackbar.AddSuccess("Add User to Role Succeeded");

            await ReloadDataAsync();
        }
    }
}
