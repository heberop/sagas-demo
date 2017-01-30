using System;
using System.Threading.Tasks;
using NServiceBus;
using SagaDemo.Pedidos.Events;
using SagaDemo.Pedidos.Helpers;

namespace SagaDemo.Pedidos.Handlers
{
    public class PedidoRecebidoPeloEstoqueHandler : IHandleMessages<PedidoRecebidoEvent>
    {
        public Task Handle(PedidoRecebidoEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine("Handler: ".Cyan().Bold() + $"Pedido recebido pelo estoque {message.Id}".Yellow());
            return Task.CompletedTask;
        }
    }
}