using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using System.Security.Claims;

namespace CRUD.ManagementUser.Bsui.Pages.Users.Components;

public partial class DialogShowClaims
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = default!;

    private IList<Claim> Claims = default!;

    [Parameter]
    public IdentityUser IdentityUser { get; set; } = default!;

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    protected override async Task OnInitializedAsync()
    {
        Claims = await _userManager.GetClaimsAsync(IdentityUser);
    }
}