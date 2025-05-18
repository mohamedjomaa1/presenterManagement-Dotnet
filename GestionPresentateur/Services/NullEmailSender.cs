using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace GestionPresentateur.Services
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Do nothing (stub implementation)
            return Task.CompletedTask;
        }
    }
}