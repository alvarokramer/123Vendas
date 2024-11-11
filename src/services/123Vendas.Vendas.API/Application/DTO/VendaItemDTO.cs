using _123Vendas.Vendas.Domain.Vendas;

namespace _123Vendas.Vendas.API.Application.DTO
{
    public class VendaItemDTO
    {
        public Guid VendaId { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }

        public static VendaItem MapearVendaItem(VendaItemDTO vendaItemDTO)
        {
            return new VendaItem(vendaItemDTO.ProdutoId, vendaItemDTO.Nome, vendaItemDTO.Quantidade,
                vendaItemDTO.ValorUnitario);
        }
    }
}
