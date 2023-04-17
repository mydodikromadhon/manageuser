using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CRUD.ManagementUser.Bsui.Areas.Identity.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    [BindProperty]
    public LoginViewModel Input { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            _logger.LogInformation("User logged in.");

            // Simpan data user nya ke dalam session / cookies

            return LocalRedirect(returnUrl);
        }

        if (result.RequiresTwoFactor)
        {
            return RedirectToPage("./LoginWith2fa", new
            {
                ReturnUrl = returnUrl,
                Input.RememberMe
            });
        }

        if (result.IsLockedOut)
        {
            _logger.LogWarning("User account locked out.");

            return RedirectToPage("./Lockout");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return Page();
        }
    }
}

public class User
{
    public string AuthenticationScheme { get; set; }
    public string Username { get; private set; }
    public string PictureUrl { get; set; }

    public User(string authenticationScheme, string username, string pictureUrl)
    {
        AuthenticationScheme = authenticationScheme;
        Username = username;
        PictureUrl = pictureUrl;
    }

    public ClaimsPrincipal Principal
    {
        get
        {
            List<Claim> claims = new()
            {
                    new Claim("Username", Username),
                    new Claim("PictureUrl", PictureUrl)
                };

            ClaimsIdentity claimsIdentity = new(claims, this.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}

public class LoginViewModel
{
    [Required]
    public string Email { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Required]
    public bool RememberMe { get; set; } = default!;
}
