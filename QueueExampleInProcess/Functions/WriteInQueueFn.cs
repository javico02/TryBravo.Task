using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Entities;
using System.IO;
using System.Threading.Tasks;

namespace QueueExampleInProcess.Functions
{
    public static class WriteInQueueFn
    {
        [FunctionName(nameof(WriteInQueueFn))]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req
            , [Queue("emailQueue"), StorageAccount("AzureWebJobsStorage")] ICollector<EmailMessage> msgs
            , ILogger logger)
        {
            logger.LogInformation($"Request arrived!");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var emailMsgs = JsonConvert.DeserializeObject<EmailMessage[]>(requestBody);

            if (emailMsgs?.Length == 0)
                return new BadRequestObjectResult("There is no message to enqueue.");

            foreach (var emailMsg in emailMsgs)
                msgs.Add(emailMsg);

            return new OkObjectResult($"{emailMsgs.Length} email messages enqueued");
        }
    }
}
