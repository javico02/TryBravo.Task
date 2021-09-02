﻿using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace QueueExampleOutOfProcess
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                            .ConfigureFunctionsWorkerDefaults()
                            .Build();

            await host.RunAsync();
        }
    }
}
