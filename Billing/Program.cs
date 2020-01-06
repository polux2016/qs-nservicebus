using System;
using System.Threading.Tasks;
using NServiceBus;
using Shared;

namespace Billing
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "Billing";

            var endpointConfiguration = new EndpointConfiguration("Billing");

            GlobalConfiguration.SetTransport(endpointConfiguration);

            await GlobalConfiguration.ReconfigureTransport(endpointConfiguration);
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
                
        }
    }
}