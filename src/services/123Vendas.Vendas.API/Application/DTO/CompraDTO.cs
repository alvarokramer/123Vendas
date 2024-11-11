using _123Vendas.Vendas.Domain.Vendas;
using Microsoft.AspNetCore.Mvc.Razor;

namespace _123Vendas.Vendas.API.Application.DTO;

public class CompraDTO
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public int NumeroCompra { get; set; }
    public DateTime DataCompra { get; set; }

    public decimal ValorTotal { get; set; }
    public decimal Desconto { get; set; }
    public string Filial { get; set; }
    public string Status { get; set; }

    public List<CompraItemDTO> CompraItens { get; set; }

    public static CompraDTO MapearParaCompraDTO(Compra compra)
    {
        var compraDTO = new CompraDTO
        {
            Id = compra.Id,
            ClienteId = compra.ClienteId,
            NumeroCompra = compra.NumeroCompra,
            Status = ObterStatus((int)compra.CompraStatus),
            DataCompra = compra.DataCompra,
            ValorTotal = compra.ValorTotal,
            Desconto = compra.Desconto,
            Filial = compra.Filial,
            CompraItens = new List<CompraItemDTO>()          
        };

        foreach (var item in compra.CompraItens)
        {
            compraDTO.CompraItens.Add(new CompraItemDTO
            {
                CompraId = item.CompraId,
                Nome = item.ProdutoNome,                
                Quantidade = item.Quantidade,
                ProdutoId = item.ProdutoId,
                ValorUnitario = item.ValorUnitario,
                ValorTotal = item.ValorTotal                                
            });
        }       

        return compraDTO;
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
