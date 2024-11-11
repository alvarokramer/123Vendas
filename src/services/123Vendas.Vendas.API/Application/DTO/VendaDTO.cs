using _123Vendas.Vendas.Domain.Vendas;
using Microsoft.AspNetCore.Mvc.Razor;

namespace _123Vendas.Vendas.API.Application.DTO;

public class VendaDTO
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public int NumeroVenda { get; set; }
    public DateTime DataVenda { get; set; }

    public decimal ValorTotal { get; set; }
    public decimal Desconto { get; set; }
    public string Filial { get; set; }
    public string Status { get; set; }

    public List<VendaItemDTO> VendaItens { get; set; }

    public static VendaDTO ParaVendaDTO(Venda venda)
    {
        var vendaDTO = new VendaDTO
        {
            Id = venda.Id,
            ClienteId = venda.ClienteId,
            NumeroVenda = venda.NumeroVenda,
            Status = ObterStatus((int)venda.VendaStatus),
            DataVenda = venda.DataVenda,
            ValorTotal = venda.ValorTotal,
            Desconto = venda.Desconto,
            Filial = venda.Filial,
            VendaItens = new List<VendaItemDTO>()          
        };

        foreach (var item in venda.VendaItens)
        {
            vendaDTO.VendaItens.Add(new VendaItemDTO
            {
                VendaId = item.VendaId,
                Nome = item.ProdutoNome,                
                Quantidade = item.Quantidade,
                ProdutoId = item.ProdutoId,
                ValorUnitario = item.ValorUnitario,
                ValorTotal = item.ValorTotal                                
            });
        }       

        return vendaDTO;
    }

    public static string ObterStatus(int status) =>
        status switch
        {
            1 => "Aprovado",
            2 => "Pago",
            3 => "Recusado",
            4 => "Cancelado",
            _ => throw new ArgumentException("Status de compra inválido", nameof(status))
        };
}
