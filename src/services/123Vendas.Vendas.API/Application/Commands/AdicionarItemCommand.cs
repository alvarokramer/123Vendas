using MediatR;

namespace _123Vendas.Vendas.API.Application.Commands
{
    public class AdicionarItemCommand : IRequest
    {
        public Guid VendaId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public AdicionarItemCommand(Guid vendaId, Guid produtoId, string nome, int quantidade, decimal valorUnitario)
        {
            VendaId = vendaId;
            ProdutoId = produtoId;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }
    }
}
