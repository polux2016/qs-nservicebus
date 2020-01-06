using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Transport.SQLServer;
using NServiceBus.Persistence.Sql;

namespace Shared
{
    public static class GlobalConfiguration
    {
        private static string SqlDBConnectionString = "Server = .; Database = NServiceBusHost; "
                + "User Id = sa; Password=yourStrong(!)Password; Max Pool Size=80;";

        private static bool IsReconfigureMode = false;

        public static TransportExtensions<SqlServerTransport> SetTransport(EndpointConfiguration endpointConfiguration)
        {
            if (IsReconfigureMode)
                endpointConfiguration.EnableInstallers();
            var transport = endpointConfiguration.UseTransport<SqlServerTransport>();
            transport.Transactions(TransportTransactionMode.SendsAtomicWithReceive);
            transport.ConnectionString(SqlDBConnectionString);
            transport.DefaultSchema("dbo");
            transport.UseSchemaForQueue("error", "dbo");
            transport.UseSchemaForQueue("audit", "dbo");
            var nativeDelayedDelivery = transport.NativeDelayedDelivery();
            nativeDelayedDelivery.TableSuffix("DD");
            nativeDelayedDelivery.DisableTimeoutManagerCompatibility();
            var persistence = endpointConfiguration.UsePersistence<InMemoryPersistence>();
            var subscriptions = transport.SubscriptionSettings();
            subscriptions.SubscriptionTableName(
                tableName: "Subscriptions",
                schemaName: "dbo");
            return transport;
        }

        public static async Task ReconfigureTransport(EndpointConfiguration endpointConfiguration)
        {
            if (IsReconfigureMode)
            {
                // This will run the installers but not start the instance.
                await Endpoint.Create(endpointConfiguration)
                    .ConfigureAwait(false);
                Environment.Exit(0);
            }
        }
    }
}
