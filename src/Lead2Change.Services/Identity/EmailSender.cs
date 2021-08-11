using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Lead2Change.Services.Identity
{
    public class EmailSender : IEmailSender
    {
        public const string DefaultSender = "test@example.com";
        public const string DefaultSenderTitle = "Lead2Change Student Registration";

        private const string _apiKey = "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";

        public async Task<Response> SendEmailAsync(string email, string subject, string htmlMessage, string sender, string senderTitle)
        {
            return await Email(sender,
                email,
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
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(sender, senderTitle);
            var subject = xSubject;

            var to = new EmailAddress(receiver, receiverTitle);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, xPlainTextContent, xHtmlContent);
            var result = await client.SendEmailAsync(msg);
            return result;
        }
        public static async Task DefaultEmail(string receiverEmail,string subject,string content,string receiverName)
        {
            await Email(DefaultSender,receiverEmail,subject,content,content,DefaultSenderTitle,receiverName);
        }
    }
}
