using System;
using NServiceBus;

namespace SagaDemo.Pedidos.Events
{
    public class PagamentoEfetuadoEvent : IEvent
    {
        public Guid Id { get; set; }

        public PagamentoEfetuadoEvent()
        {

        }
        public PagamentoEfetuadoEvent(Guid id)
        {
            this.Id = id;
        }

    }
}