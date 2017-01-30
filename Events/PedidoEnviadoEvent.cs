using System;
using NServiceBus;

namespace SagaDemo.Pedidos.Events
{
    public class PedidoEnviadoEvent : IEvent
    {
        public Guid Id { get; set; }
        public PedidoEnviadoEvent()
        {

        }
        public PedidoEnviadoEvent(Guid id)
        {
            this.Id = id;
        }
    }
}