using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Shared.Entities;

namespace QueueExampleOutOfProcess.Functions
{
    public static class SendEmailUsingServiceFn
    {
        [Function(nameof(SendEmailUsingServiceFn))]
        [SendGridOutput(ApiKey = "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")]
        public static void Run([QueueTrigger("emailqueue", Connection = "AzureWebJobsStorage")] EmailMessage emailMessage
            , FunctionContext context)
        {
            var logger = context.GetLogger("HttpFunction");
            logger.LogInformation($"Email from: {emailMessage.SenderEmail}");
        }
    }
}
