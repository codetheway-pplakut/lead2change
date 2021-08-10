using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Lead2Change.Domain.Models;
using Lead2Change.Domain.Constants;

namespace Lead2Change.Web.Ui.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly SignInManager<AspNetUsers> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<AspNetUsers> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<AspNetUsers> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            if (User != null)
            {
                if (User.IsInRole(StringConstants.RoleNameAdmin))
                {
                    return LocalRedirect("~/Admin/Index");
                } else if (User.IsInRole(StringConstants.RoleNameCoach)) {
                    return LocalRedirect("~/Coaches/CoachesStudents");
                }
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;

            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (await _userManager.IsInRoleAsync(user, StringConstants.RoleNameAdmin))
                    {
                        returnUrl = returnUrl ??
                        ((await _userManager.GetRolesAsync(user)).Contains(StringConstants.RoleNameAdmin)
                        ? Url.Content("~/Admin/Index")
                        : Url.Content("~/"));
                    } else if (await _userManager.IsInRoleAsync(user, StringConstants.RoleNameCoach))
                    {
                        returnUrl = returnUrl ??
                        ((await _userManager.GetRolesAsync(user)).Contains(StringConstants.RoleNameCoach)
                        ? Url.Content("~/Coaches/CoachesStudents")
                        : Url.Content("~/"));
                    } else
                    {
                        returnUrl = Url.Content("~/");
                    }
                        
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
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

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
