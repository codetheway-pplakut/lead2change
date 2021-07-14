using Lead2Change.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Lead2Change.Web.Ui.Controllers
{
    public class _BaseController : Controller
    {
        IIdentityService _identityService;

        public _BaseController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task Email(string sender, string receiver, string xSubject, string xPlainTextContent, string xHtmlContent, string senderTitle, string receiverTitle)
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

