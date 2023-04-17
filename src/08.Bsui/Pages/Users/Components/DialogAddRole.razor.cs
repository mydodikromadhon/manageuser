﻿using CRUD.ManagementUser.Bsui.Common.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using MudBlazor;

namespace CRUD.ManagementUser.Bsui.Pages.Users.Components;

public partial class DialogAddRole
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter]
    public IdentityUser Request { get; set; } = default!;

    public List<IdentityRole> IdentityRoles { get; set; } = default!;

    public string SelectedRole { get; set; } = default!;

    private bool _isLoading;

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    protected override void OnInitialized()
    {
        GetListOfRoles();
    }

    private void GetListOfRoles()
    {
        _isLoading = true;

        IdentityRoles = _roleManager.Roles.ToList();

        _isLoading = false;
    }

    private async Task OnValidSubmit()
    {
        _isLoading = true;

        var response = await _userManager.AddToRoleAsync(Request, SelectedRole);

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
