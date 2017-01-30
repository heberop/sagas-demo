using System;
using NServiceBus;

namespace SagaDemo.Pedidos
{
    public class PedidoSagaData : ContainSagaData
    {
        public virtual Guid PedidoId { get; set; }
    }
}