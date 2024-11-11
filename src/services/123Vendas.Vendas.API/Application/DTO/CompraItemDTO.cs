using _123Vendas.Vendas.Domain.Vendas;

namespace _123Vendas.Vendas.API.Application.DTO
{
    public class CompraItemDTO
    {
        public Guid CompraId { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }

        public static CompraItem MapearCompraItem(CompraItemDTO compraItemDTO)
        {
            return new CompraItem(compraItemDTO.ProdutoId, compraItemDTO.Nome, compraItemDTO.Quantidade,
                compraItemDTO.ValorUnitario);
        }
    }
}
