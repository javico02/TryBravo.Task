using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using Shared.Entities;

namespace QueueExampleInProcess.Functions
{
    public class SendEmailUsingBinding
    {
        [FunctionName(nameof(SendEmailUsingBinding))]
        public static void Run([QueueTrigger("emailQueue", Connection = "AzureWebJobsStorage")] EmailMessage emailMessage
            , [SendGrid(ApiKey = "SendgridAPIKey")] out SendGridMessage sgMsg
            , ILogger logger)
        {
            // Creating email message
            sgMsg = new SendGridMessage()
            {
                From = new EmailAddress(emailMessage.SenderEmail),
                Subject = emailMessage.Subject,
                PlainTextContent = emailMessage.PlainText,
                HtmlContent = emailMessage.HtmlContent
            };

            // Iterating to add all receivers
            foreach (var receiverEmail in emailMessage.ReceiverEmails)
            {
                sgMsg.AddTo(new EmailAddress(receiverEmail));
            }

            logger.LogInformation("Email sent successfully!");
        }
    }
}
