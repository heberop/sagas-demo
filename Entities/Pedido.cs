using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaDemo.Pedidos.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public DateTime Data { get; private set; }
        public Cliente Cliente { get; private set; }
        public IEnumerable<PedidoItem> Itens { get; private set; }
        public Endereco EnderecoEntrega { get; private set; }

        public Pedido(Cliente cliente)
        {
            this.Id = Guid.NewGuid();
            this.Cliente = cliente;
            this.Data = DateTime.Today;
            this.Itens = new List<PedidoItem>();
        }
        public PedidoItem AdicionarItem(Guid produtoId, int quantidade)
        {
            throw new NotImplementedException();
        }
        public void DefinirEnderecoEntrega(Endereco endereco)
        {
            this.EnderecoEntrega = endereco;
        }
    }

    public class Endereco
    {
    }

    public class PedidoItem
    {
    }

    public class Cliente
    {
    }
}
