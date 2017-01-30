using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SagaDemo.Pedidos.Commands;
using SagaDemo.Pedidos.Helpers;

namespace SagaDemo.Pedidos
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureAndSend().GetAwaiter().GetResult();
        }

        private static async Task ConfigureAndSend()
        {
            EscapeSequencer.Install(); 
            EscapeSequencer.Bold = true;

            var defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Warn);

            while (true)
            {
                var endpointConfiguration = new EndpointConfiguration("ExemploSaga.Pedidos");

                endpointConfiguration.SendFailedMessagesTo("ExemploSaga.Pedidos.Errors");
                /* Para testes, é possível usar persistencia em memória */
                //endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Subscriptions>();
                //endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Timeouts>();
                //endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Outbox>();
                //endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.GatewayDeduplication>();
                //endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Sagas>();
                endpointConfiguration.UsePersistence<NHibernatePersistence>();

                endpointConfiguration.UseSerialization<JsonSerializer>();
                endpointConfiguration.EnableInstallers();
                
                endpointConfiguration.SagaPlugin("OpenPlatform.ServiceControl");
                

                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                transport.ConnectionString("host=localhost");

                var endpoint = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

                var adicionarPedido = new AdicionarPedidoCommand(Guid.NewGuid(), "Heber");

                Console.WriteLine($"ENTER para enviar mensagem para pedido {adicionarPedido.Id} - {adicionarPedido.Cliente}");
                Console.ReadLine();

                await endpoint.SendLocal(adicionarPedido);

                Console.WriteLine("Aguardando os handlers... ENTER para finalizar");

                await Task.Delay(300).ConfigureAwait(false);
            }
        }
    }
}
