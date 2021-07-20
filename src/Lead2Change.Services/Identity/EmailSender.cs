using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace Lead2Change.Services.Identity
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
            // Send the email lol
        }
    }
}
