using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Lead2Change.Domain.Models;
using Lead2Change.Domain.Constants;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Students;
using Lead2Change.Web.Ui.Models;
using Lead2Change.Services.Coaches;

namespace Lead2Change.Web.Ui.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterCoachModel : PageModel
    {
        private readonly SignInManager<AspNetUsers> _signInManager;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly ICoachService _coachService;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterCoachModel(
            UserManager<AspNetUsers> userManager,
            SignInManager<AspNetUsers> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ICoachService coachService)
        {
            _userManager = userManager;
            _coachService = coachService;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class PasswordLowercaseCharRequired : RegularExpressionAttribute
        {
            public PasswordLowercaseCharRequired(string pattern) : base(pattern) { }
        }


        public class PasswordUppercaseCharRequired : RegularExpressionAttribute
        {
            public PasswordUppercaseCharRequired(string pattern) : base(pattern) { }
        }

        public class PasswordNumberCharRequired : RegularExpressionAttribute
        {
            public PasswordNumberCharRequired(string pattern) : base(pattern) { }
        }

        public class PasswordSpecialCharRequired : RegularExpressionAttribute
        {
            public PasswordSpecialCharRequired(string pattern) : base(pattern) { }
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{6,}$", ErrorMessage = "Password must have at least one lowercase letter, at least one uppercase letter, at least one number, and at least one special character, and be at least 6 characters long")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string CoachFirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string CoachLastName { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            [DataType(DataType.PhoneNumber)]
            public string CoachPhoneNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var isMSValid = ModelState.IsValid;
            if (ModelState.IsValid)
            {
                var userWithEmail = await _userManager.FindByEmailAsync(Input.Email);
                if (userWithEmail == null)
                {

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There is already a user registered with this email");
                    return Page();
                }
                Coach coach = new Coach()
                {
                    CoachFirstName = Input.CoachFirstName,
                    CoachLastName = Input.CoachLastName,
                    CoachEmail = Input.Email,
                    CoachPhoneNumber = Input.CoachPhoneNumber,
                    Active = true,
                    Students = new List<Student>()
                };
                var result1 = await _coachService.Create(coach);
                if (User.IsInRole(StringConstants.RoleNameCoach))
                {
                    await _coachService.Update(result1);
                    return RedirectToAction("Index", "Students");
                }

                var user = new AspNetUsers { UserName = Input.Email, Email = Input.Email, AssociatedId = result1.Id };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, StringConstants.RoleNameCoach);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code1 = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code1));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code1, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                    var result2 = await _userManager.ConfirmEmailAsync(user, code);
                    if (result2.Succeeded)
                    {
                        return RedirectToAction("Index", "Coaches");
                    }
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        //                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
