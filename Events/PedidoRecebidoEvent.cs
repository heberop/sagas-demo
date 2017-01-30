using System;
using NServiceBus;

namespace SagaDemo.Pedidos.Events
{
    public class PedidoRecebidoEvent : IEvent
    {
        public Guid Id { get; set; }

        public PedidoRecebidoEvent()
        {
        }

        public PedidoRecebidoEvent(Guid id)
        {
            this.Id = id;
        }
    }
}