using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using Lead2Change.Services.Identity;
using Lead2Change.Web.Ui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Lead2Change.Web.Ui.Controllers
{
    public class _BaseController : Controller
    {
        public IUserService IdentityService;
        public RoleManager<AspNetRoles> RoleManager;
        public UserManager<AspNetUsers> UserManager;
        public SignInManager<AspNetUsers> SignInManager;

        public _BaseController(IUserService identityService, RoleManager<AspNetRoles> roleManager, UserManager<AspNetUsers> userManager, SignInManager<AspNetUsers> signInManager)
        {
            IdentityService = identityService;
            RoleManager = roleManager;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        /// <summary>
        /// This is an example of how to create roles
        /// </summary>
        /// <returns></returns>
        protected async Task CreateDefaultRoles()
        {
            var hasAdmin = await RoleManager.FindByNameAsync(StringConstants.RoleNameAdmin);

            if (hasAdmin == null)
                await RoleManager.CreateAsync(new AspNetRoles()
                {
                    Name = StringConstants.RoleNameAdmin,
                    NormalizedName = StringConstants.RoleNameAdmin
                });

            var hasCoach = await RoleManager.FindByNameAsync(StringConstants.RoleNameCoach);

            if (hasCoach == null)
                await RoleManager.CreateAsync(new AspNetRoles()
                {
                    Name = StringConstants.RoleNameCoach,
                    NormalizedName = StringConstants.RoleNameCoach
                });

            var hasStudent = await RoleManager.FindByNameAsync(StringConstants.RoleNameStudent);

            if (hasStudent == null)
                await RoleManager.CreateAsync(new AspNetRoles()
                {
                    Name = StringConstants.RoleNameStudent,
                    NormalizedName = StringConstants.RoleNameStudent
                });
        }

        /// <summary>
        /// THIS IS NOT CALLED WHEN A USER USES THE WEBSITE TO CREATE A USER
        /// 
        /// This is used for creating a user on the backend, programmatically
        /// </summary>
        /// <param name = "email"></param>
        /// <returns></returns>
        protected async Task CreateNewUser(string email, string password, string roleName, bool confirm = true)
        {
            var identityUser = new AspNetUsers()
            {
                UserName = email,
                Email = email
            };

            var result = await UserManager.CreateAsync(identityUser, password);
            if (confirm)
            {
                var token = await UserManager.GenerateEmailConfirmationTokenAsync(identityUser);
                var confirmationEmail = await UserManager.ConfirmEmailAsync(identityUser, token);
            }

            if (result.Succeeded)
            {
                var user = await UserManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var role = await UserManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    //Do something because the user does not exist
                }
            }
            else
            {
                //Do something because the user could not be created
            }
        }

        protected async Task<bool> CanEditStudent(Guid studentId)
        {
            if (SignInManager.IsSignedIn(User))
            {
                if (User.IsInRole(StringConstants.RoleNameStudent))
                {
                    var user = await UserManager.GetUserAsync(User);
                    return studentId == user.AssociatedId;
                }
                else if (User.IsInRole(StringConstants.RoleNameCoach))
                {
                    // TODO: Return only if coach "owns" this student
                    return true;
                }
                else if (User.IsInRole(StringConstants.RoleNameAdmin))
                {
                    return true;
                }
            }
            return false;
        }

        protected ViewResult Error(ErrorViewModel e)
        {
            return View("Error", e);
        }

        /* THIS FUNCTION IS A WORK IN PROGRESS
        protected async Task Email(string sender, string receiver, string xSubject, string xPlainTextContent, string xHtmlContent, string senderTitle, string receiverTitle)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(sender, senderTitle);
            var subject = xSubject;

            var to = new EmailAddress(receiver, receiverTitle);
            var plainTextContent = xPlainTextContent;
            var htmlContent = xHtmlContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        } 
        */
    }
}
