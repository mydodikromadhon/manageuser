using CRUD.ManagementUser.Application.Users.Queries.GetUsers;
using CRUD.ManagementUser.Bsui.Common.Components;
using CRUD.ManagementUser.Bsui.Common.Extensions;
using CRUD.ManagementUser.Bsui.Pages.Users.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;

namespace CRUD.ManagementUser.Bsui.Pages.Users;

public partial class Users
{
    public GetUsersResponse Response { get; set; } = new();

    private bool _isLoading;

    protected override async Task OnInitializedAsync()
    {
        await ReloadDataAsync();
    }

    private async Task ReloadDataAsync()
    {
        _isLoading = true;

        Response = await Sender.Send(new GetUsersQuery());

        _isLoading = false;
    }

    private async Task ShowDialogAddUsers()
    {
        var identityUser = new IdentityUser();

        var parameters = new DialogParameters
        {
            { nameof(DialogAdd.Request), identityUser }
        };

        var dialog = _dialogService.Show<DialogAdd>($"Create new User", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            _snackbar.AddSuccess("New User Created");

            await ReloadDataAsync();
        }
    }

    private void ShowUserRoles(IdentityUser identityUser)
    {
        var parameters = new DialogParameters
        {
            { nameof(DialogUserRoles.IdentityUser), identityUser }
        };

        _dialogService.Show<DialogUserRoles>($"{identityUser.UserName} Roles", parameters);
    }

    private async Task ShowResetPassword(IdentityUser identityUser)
    {
        var dialog = _dialogService.Show<DialogResetPassword>($"Reset Password {identityUser.UserName}");
        
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
            var response = await _userManager.ResetPasswordAsync(identityUser, resetToken, "P@ssw0rd0!");

            if (response.Errors.Any())
            {
                _snackbar.AddErrors(response.Errors.Select(x => x.Description));

                return;
            }
        }
    }

    private async Task ShowDialogDelete(IdentityUser identityUser)
    {
        var dialog = _dialogService.Show<DialogDelete>($"Delete User {identityUser.UserName}");
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await _userManager.DeleteAsync(identityUser);

            _snackbar.AddSuccess("Delete User Succeeded");

            await ReloadDataAsync();
        }
    }
}
