using System;
using NServiceBus;

namespace SagaDemo.Pedidos.Commands
{
    public class ProcessarPagamentoCommand : ICommand
    {
        public Guid Id { get; set; }
        public ProcessarPagamentoCommand()
        {

        }
        public ProcessarPagamentoCommand(Guid id)
        {
            this.Id = id;
        }
    }
}