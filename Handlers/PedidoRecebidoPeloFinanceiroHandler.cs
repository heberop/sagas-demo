using System;
using System.Threading.Tasks;
using NServiceBus;
using SagaDemo.Pedidos.Events;
using SagaDemo.Pedidos.Helpers;

namespace SagaDemo.Pedidos.Handlers
{
    public class PedidoRecebidoPeloFinanceiroHandler : IHandleMessages<PedidoRecebidoEvent>
    {
        public Task Handle(PedidoRecebidoEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine("Handler: ".Cyan().Bold() + $"Pedido recebido pelo financeiro {message.Id}".Yellow());
            return Task.FromResult(0);
        }
    }
}