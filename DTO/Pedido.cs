using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaDemo.Pedidos.DTO
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public Guid ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public IEnumerable<PedidoItem> Itens { get; set; }
        public Endereco EnderecoEntrega { get; set; }
    }

    public class Endereco
    {
    }

    public class PedidoItem
    {
    }
}
