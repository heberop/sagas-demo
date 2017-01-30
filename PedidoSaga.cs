using System;
using System.Threading.Tasks;
using NServiceBus;
using SagaDemo.Pedidos.Commands;
using SagaDemo.Pedidos.Events;
using SagaDemo.Pedidos.Helpers;
using SagaDemo.Pedidos.Messages;

namespace SagaDemo.Pedidos
{
    public class PedidoSaga : Saga<PedidoSagaData>,
        IAmStartedByMessages<AdicionarPedidoCommand>,
        IHandleMessages<PedidoRecebidoEvent>,
        IHandleTimeouts<PedidoTimeout>,
        IHandleMessages<ReservarItensDoPedidoCommand>,
        IHandleMessages<ProcessarPagamentoCommand>,
        IHandleMessages<PagamentoEfetuadoEvent>,
        IHandleMessages<EnviarPedidoCommand>,
        IHandleMessages<PedidoEnviadoEvent>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PedidoSagaData> mapper)
        {
            mapper.ConfigureMapping<AdicionarPedidoCommand>(message => message.Id).ToSaga(saga => saga.PedidoId);
            mapper.ConfigureMapping<PedidoRecebidoEvent>(message => message.Id).ToSaga(saga => saga.PedidoId);
            mapper.ConfigureMapping<ReservarItensDoPedidoCommand>(message => message.Id).ToSaga(saga => saga.PedidoId);
            mapper.ConfigureMapping<ProcessarPagamentoCommand>(message => message.Id).ToSaga(saga => saga.PedidoId);
            mapper.ConfigureMapping<PagamentoEfetuadoEvent>(message => message.Id).ToSaga(saga => saga.PedidoId);
            mapper.ConfigureMapping<EnviarPedidoCommand>(message => message.Id).ToSaga(saga => saga.PedidoId);
            mapper.ConfigureMapping<PedidoEnviadoEvent>(message => message.Id).ToSaga(saga => saga.PedidoId);
        }

        public Task Handle(AdicionarPedidoCommand message, IMessageHandlerContext context)
        {
            RequestTimeout<PedidoTimeout>(context, TimeSpan.FromMilliseconds(5000));

            WriteToConsole(message, message.Id);

            return context.Publish(new PedidoRecebidoEvent(message.Id));
        }

        public Task Handle(PedidoRecebidoEvent message, IMessageHandlerContext context)
        {
            WriteToConsole(message, message.Id);

            return context.SendLocal(new ReservarItensDoPedidoCommand(message.Id));
        }

        public Task Timeout(PedidoTimeout state, IMessageHandlerContext context)
        {
            Console.WriteLine($"SAGA: TIMEOUT!!! {state.Id}");

            MarkAsComplete();

            return Task.CompletedTask;
        }

        public Task Handle(ReservarItensDoPedidoCommand message, IMessageHandlerContext context)
        {
            //crash em alguns casos, antes de tratar o handle de pedido recebido
            Console.WriteLine("SAGA:".Yellow() + $" Tentando reservar {message.Id}".Cyan());
            try
            {
                var random = new Random().Next(0, 2);
                int x = 10 / random;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRO CRITICO!!!!!".Yellow().Bold());
                throw;
            }

            WriteToConsole(message, message.Id);

            return context.SendLocal(new ProcessarPagamentoCommand(message.Id));
        }

        public Task Handle(ProcessarPagamentoCommand message, IMessageHandlerContext context)
        {
            WriteToConsole(message, message.Id);
            return context.Publish(new PagamentoEfetuadoEvent(message.Id));
        }

        public Task Handle(PagamentoEfetuadoEvent message, IMessageHandlerContext context)
        {
            WriteToConsole(message, message.Id);
            return context.SendLocal(new EnviarPedidoCommand(message.Id));
        }

        public Task Handle(EnviarPedidoCommand message, IMessageHandlerContext context)
        {
            WriteToConsole(message, message.Id);
            return context.Publish(new PedidoEnviadoEvent(message.Id));
        }

        public Task Handle(PedidoEnviadoEvent message, IMessageHandlerContext context)
        {
            WriteToConsole(message, message.Id);

            this.MarkAsComplete();

            return Task.CompletedTask;
        }

        private static void WriteToConsole(ICommand command, Guid messageId)
        {
            Console.WriteLine("SAGA:".Yellow() + $" Comando '{command.GetType().Name}' ".Green() + messageId.ToString().Italic());
        }
        private static void WriteToConsole(IEvent command, Guid messageId)
        {
            Console.WriteLine("SAGA:".Yellow() + $" Evento '{command.GetType().Name}' publicado ".Green() + messageId.ToString().Italic());
        }
    }
}
