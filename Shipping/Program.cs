using NServiceBus;
using System;
using System.Threading.Tasks;
using Shared;

namespace Shipping
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "Shipping";

            var endpointConfiguration = new EndpointConfiguration("Shipping");

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