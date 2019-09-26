using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(EmailMessage message)
        {
            return Task.CompletedTask;
        }
    }
}