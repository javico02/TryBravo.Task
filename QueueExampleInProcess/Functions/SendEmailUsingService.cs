using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using QueueExampleInProcess.Services;
using Shared.Entities;
using System;
using System.Threading.Tasks;

namespace QueueExampleInProcess.Functions
{
    public class SendEmailUsingService
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<SendEmailUsingService> _logger;

        public SendEmailUsingService(IEmailSender emailSender, ILogger<SendEmailUsingService> logger)
        {
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender), "Email sender can't be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger can't be null.");
        }

        [FunctionName(nameof(SendEmailUsingService))]
        public async Task Run([QueueTrigger("emailQueue", Connection = "AzureWebJobsStorage")] EmailMessage emailMsg)
        {
            // Sending email and receiving whether email was sent or not
            var isSent = await _emailSender.SendEmailAsync(emailMsg);

            _logger.LogInformation(isSent ? "Email sent successfully!" : "Email wasn't sent.");
        }
    }
}
