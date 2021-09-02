using Shared.Entities;
using System.Threading.Tasks;

namespace QueueExampleInProcess.Services
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(EmailMessage emailMessage);
    }
}
