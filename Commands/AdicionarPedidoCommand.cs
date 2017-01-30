using System;
using NServiceBus;

namespace SagaDemo.Pedidos.Commands
{
    public class AdicionarPedidoCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Cliente { get; set; }

        public AdicionarPedidoCommand()
        {
            
        }
        public AdicionarPedidoCommand(Guid id, string cliente)
        {
            this.Id = id;
            Cliente = cliente;
        }
    }
}