using System;
using NServiceBus;

namespace SagaDemo.Pedidos.Commands
{
    public class EnviarPedidoCommand : ICommand
    {
        public Guid Id { get; set; }
        public EnviarPedidoCommand()
        {

        }
        public EnviarPedidoCommand(Guid id)
        {
            this.Id = id;
        }
    }
}