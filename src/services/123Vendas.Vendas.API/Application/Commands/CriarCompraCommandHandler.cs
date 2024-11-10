using _123Vendas.Vendas.API.Application.DTO;
using _123Vendas.Vendas.Domain.Vendas;
using MediatR;

namespace _123Vendas.Vendas.API.Application.Commands;

public class CriarCompraCommandHandler : IRequestHandler<CriarCompraCommand>
{
    private readonly IVendaRepository _vendaRepository;

    public CriarCompraCommandHandler(IVendaRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public async Task Handle(CriarCompraCommand message, CancellationToken cancellationToken)
    {
        var venda = MapearCompra(message);
        venda.CalcularValorVenda();
        venda.AutorizarVenda();

        _vendaRepository.Adicionar(venda);

        await _vendaRepository.UnitOfWork.Commit();
    }

    private Venda MapearCompra(CriarCompraCommand message)
    {
        var venda = new Venda(message.ClienteId, message.ValorTotal, 
            message.VendaItens.Select(VendaItemDTO.MapearVendaItem).ToList(),
            message.Filial, message.Desconto);
        
        return venda;
    }
}
