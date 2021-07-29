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
    }
}
