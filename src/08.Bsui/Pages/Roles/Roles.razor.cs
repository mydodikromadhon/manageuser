using CRUD.ManagementUser.Bsui.Common.Components;
using CRUD.ManagementUser.Bsui.Common.Extensions;
using CRUD.ManagementUser.Bsui.Pages.Roles.Components;
using Microsoft.AspNetCore.Identity;
using MudBlazor;

namespace CRUD.ManagementUser.Bsui.Pages.Roles
{
    public partial class Roles
    {
        public List<IdentityRole> IdentityRoles { get; set; } = new();

        private bool _isLoading;

        protected override void OnInitialized()
        {
            ReloadData();
        }

        private void ReloadData()
        {
            _isLoading = true;

            IdentityRoles = _roleManager.Roles.ToList();

            _isLoading = false;
        }

        private async Task ShowDialogAdd()
        {
            var request = new IdentityRole();

            var parameters = new DialogParameters
            {
                { nameof(DialogAdd.Request), request }
            };

            var dialog = _dialogService.Show<DialogAdd>("Add New Role", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                _snackbar.AddSuccess("New Role Created");

                ReloadData();
            }
        }

        private async Task ShowDialogEdit(IdentityRole identityRole)
        {
            var parameters = new DialogParameters
            {
                { nameof(DialogAdd.Request), identityRole }
            };

            var dialog = _dialogService.Show<DialogEdit>("Edit Role", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                _snackbar.AddSuccess("Update Role Succeeded");

                ReloadData();
            }
        }

        private async Task ShowDialogDelete(IdentityRole identityRole)
        {
            var dialog = _dialogService.Show<DialogDelete>($"Delete Role {identityRole.Name}");
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await _roleManager.DeleteAsync(identityRole);

                _snackbar.AddSuccess("Delete Role Succeeded");

                ReloadData();
            }
        }

        private void ShowRolePermissions(IdentityRole identityRole)
        {
            var parameters = new DialogParameters
        {
            { nameof(DialogRolePermissions.IdentityRole), identityRole }
        };

            _dialogService.Show<DialogRolePermissions>($"{identityRole.Name} Permissions", parameters);
        }
    }
}
