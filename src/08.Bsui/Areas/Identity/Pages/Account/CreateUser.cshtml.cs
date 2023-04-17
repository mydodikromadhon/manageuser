using CRUD.ManagementUser.Domain.Entities;
using CRUD.ManagementUser.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CRUD.ManagementUser.Bsui.Areas.Identity.Pages.Account;

public class CreateUserModel : PageModel
{
    private PersistenceService _dbContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    private readonly ILogger<LoginModel> _logger;

    public CreateUserModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, PersistenceService dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _dbContext = dbContext;
    }

    [BindProperty]
    public CreateUserViewModel Input { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        string? returnUrl = null;

        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var testlah = _dbContext.Users;
            var anyEmployee = _dbContext.Employees;

            var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                await _signInManager.SignInAsync(user, isPersistent: false);

                return LocalRedirect(returnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return Page();
    }
}

public class CreateUserViewModel
{
	//[Required]
	//public string FullName { get; set; } = default!;

	//[Required]
	//public string Position { get; set; } = default!;

	[Required]
    public string Email { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;
}
