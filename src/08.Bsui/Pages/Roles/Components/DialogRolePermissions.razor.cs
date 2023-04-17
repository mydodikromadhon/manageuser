using CRUD.ManagementUser.Bsui.Common.Authorizations;
using CRUD.ManagementUser.Bsui.Common.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using System.Security.Claims;

namespace CRUD.ManagementUser.Bsui.Pages.Roles.Components;

public partial class DialogRolePermissions
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    public IList<string> PermissionRoles { get; set; } = default!;

    [Parameter]
    public IdentityRole IdentityRole { get; set; } = default!;

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

        var roleClaims = await _roleManager.GetClaimsAsync(IdentityRole);

        PermissionRoles = roleClaims
            .Where(x => x.Type == AuthorizationClaimTypes.Permission)
            .Select(x => x.Value)
            .ToList();

        _isLoading = false;
    }

    private async Task DeleteRolePermissions(string permissionName)
    {
        _isLoading = true;

        var response = await _roleManager.RemoveClaimAsync(IdentityRole, new Claim(AuthorizationClaimTypes.Permission, permissionName));

        _isLoading = false;

        if (response.Errors.Any())
        {
            _snackbar.AddErrors(response.Errors.Select(x => x.Description));

            return;
        }

        await ReloadDataAsync();
    }

    private async Task ShowDialogAddPermission()
    {
        var parameters = new DialogParameters
        {
            { nameof(DialogAddPermission.Request), IdentityRole }
        };

        var dialog = _dialogService.Show<DialogAddPermission>("Add Permission to Role", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            _snackbar.AddSuccess("Add User to Permission Succeeded");

            await ReloadDataAsync();
        }
    }
}