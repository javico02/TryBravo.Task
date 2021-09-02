using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace QueueExampleOutOfProcess.Functions
{
    public class WriteInQueueFn
    {
        [Function(nameof(WriteInQueueFn))]
        public static async Task<FunctionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req, FunctionContext context)
        {
            var logger = context.GetLogger(nameof(WriteInQueueFn));
            logger.LogInformation($"Request arrived!");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var emailMsgs = JsonConvert.DeserializeObject<EmailMessage[]>(requestBody);

            var response = emailMsgs?.Length == 0
                ? req.CreateResponse(HttpStatusCode.BadRequest)
                : req.CreateResponse(HttpStatusCode.OK);

            if (emailMsgs?.Length == 0)
            {
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.WriteString("There is no message to enqueue.");
            }
            else
            {
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.WriteString("Data enqueued!!");
            }

            return new FunctionResult
            {
                EmailMessages = emailMsgs ?? Array.Empty<EmailMessage>(),
                HttpReponse = response
            };
        }
    }

    public class FunctionResult
    {
        [QueueOutput("emailqueue", Connection = "AzureWebJobsStorage")]
        public IEnumerable<EmailMessage> EmailMessages { get; set; }

        public HttpResponseData HttpReponse { get; set; }
    }
}
