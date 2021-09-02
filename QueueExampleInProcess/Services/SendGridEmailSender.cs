using SendGrid;
using SendGrid.Helpers.Mail;
using Shared.Entities;
using System;
using System.Threading.Tasks;

namespace QueueExampleInProcess.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        public SendGridEmailSender()
        { }

        public async Task<bool> SendEmailAsync(EmailMessage emailMessage)
        {
            // Setting up
            var apiKey = Environment.GetEnvironmentVariable("SendGridApiKey");
            var client = new SendGridClient(apiKey);

            // Creating email message
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(emailMessage.SenderEmail),
                Subject = emailMessage.Subject,
                PlainTextContent = emailMessage.PlainText,
                HtmlContent = emailMessage.HtmlContent
            };

            // Iterating to add all receivers
            foreach (var receiverEmail in emailMessage.ReceiverEmails)
            {
                msg.AddTo(new EmailAddress(receiverEmail));
            }

            // Sending email
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}
