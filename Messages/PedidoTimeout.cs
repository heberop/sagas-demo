using System;
using NServiceBus;

namespace SagaDemo.Pedidos.Messages
{
    public class PedidoTimeout : IMessage
    {
        public Guid Id { get; set; }
    }
}