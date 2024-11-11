using _123Vendas.Vendas.API.Application.DTO;
using _123Vendas.Vendas.Domain.Vendas;
using MediatR;

namespace _123Vendas.Vendas.API.Application.Commands;

public class CompraCommandHandler :
    IRequestHandler<CriarCompraCommand>,
    IRequestHandler<AdicionarItemCommand>,
    IRequestHandler<RemoverItemCommand>,
    IRequestHandler<CancelarCompraCommand>
{
    private readonly IVendaRepository _vendaRepository;

    public CompraCommandHandler(IVendaRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public async Task Handle(CriarCompraCommand request, CancellationToken cancellationToken)
    {
        var venda = MapearCompra(request);
        venda.CalcularValorVenda();
        venda.AutorizarVenda();

        _vendaRepository.Adicionar(venda);

        await _vendaRepository.UnitOfWork.Commit();
    }

    public async Task Handle(AdicionarItemCommand request, CancellationToken cancellationToken)
    {
        var venda = await _vendaRepository.ObterVendaPorId(request.VendaId);
        var vendaItem = new VendaItem(request.ProdutoId, request.Nome, request.Quantidade, request.ValorUnitario);

        if (venda is null)
            throw new InvalidDataException("Compra não encontrada!");

        var pedidoItemExistente = venda.VendaItemExistente(vendaItem);
        venda.AdicionarItem(vendaItem);

        if (pedidoItemExistente)
        {
            _vendaRepository.AtualizarItem(
                venda.VendaItens.FirstOrDefault(p => p.ProdutoId == vendaItem.ProdutoId));
        }
        else
        {
            _vendaRepository.AdicionarItem(vendaItem);
        }

        await _vendaRepository.UnitOfWork.Commit();
    }

    public async Task Handle(RemoverItemCommand request, CancellationToken cancellationToken)
    {
        var venda = await _vendaRepository.ObterVendaPorId(request.VendaId);

        if (venda is null)
            throw new InvalidDataException("Compra não encontrada!");

        var vendaItem = await _vendaRepository.ObterItemPorVenda(venda.Id, request.ProdutoId);
        venda.RemoverItem(vendaItem);

        _vendaRepository.RemoverItem(vendaItem);
        _vendaRepository.Atualizar(venda);

        await _vendaRepository.UnitOfWork.Commit();
    }

    public async Task Handle(CancelarCompraCommand request, CancellationToken cancellationToken)
    {
        var venda = await _vendaRepository.ObterVendaPorId(request.VendaId);
        venda.CancelarVenda();

        _vendaRepository.Atualizar(venda);
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
