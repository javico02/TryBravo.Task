using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using QueueExampleInProcess;
using QueueExampleInProcess.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace QueueExampleInProcess
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IEmailSender, SendGridEmailSender>();
        }
    }
}
