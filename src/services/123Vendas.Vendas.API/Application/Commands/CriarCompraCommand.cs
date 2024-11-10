using MediatR;
using _123Vendas.Vendas.API.Application.DTO;

namespace _123Vendas.Vendas.API.Application.Commands;

public class CriarCompraCommand : IRequest
{    
    public Guid ClienteId { get; set; }
    public decimal ValorTotal { get; set; }
    public decimal Desconto { get; set; }
    public string Filial { get; set; }
    public List<VendaItemDTO> VendaItens { get; set; }
}
