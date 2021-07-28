using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Lead2Change.Services.Identity
{
    public class EmailSender : IEmailSender
    {
        public const string DefaultSender = "joeljk2003@gmail.com";
        public const string DefaultSenderTitle = "Lead2Change Student Registration";

        private const string _apiKey = "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";
        private static SendGridClient _client = new SendGridClient(_apiKey);

        public async Task<Response> SendEmailAsync(string email, string subject, string htmlMessage, string sender, string senderTitle)
        {
            return await Email(email,
                sender,
                subject,
                htmlMessage,
                htmlMessage,
                senderTitle, email);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendEmailAsync(email, subject, htmlMessage, DefaultSender, DefaultSenderTitle);
        }

        public static async Task<Response> Email(string sender, string receiver, string xSubject, string xPlainTextContent, string xHtmlContent, string senderTitle, string receiverTitle)
        {
            var from = new EmailAddress(sender, senderTitle);
            var subject = xSubject;

            var to = new EmailAddress(receiver, receiverTitle);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, xPlainTextContent, xHtmlContent);
            return await _client.SendEmailAsync(msg);
        }
    }
}
