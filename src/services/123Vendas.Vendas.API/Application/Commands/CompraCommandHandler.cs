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
    private readonly ICompraRepository _compraRepository;

    public CompraCommandHandler(ICompraRepository compraRepository)
    {
        _compraRepository = compraRepository;
    }

    public async Task Handle(CriarCompraCommand request, CancellationToken cancellationToken)
    {
        var compra = MapearCompra(request);
        compra.CalcularValorCompra();
        compra.AutorizarCompra();

        _compraRepository.Adicionar(compra);

        await _compraRepository.UnitOfWork.Commit();
    }

    public async Task Handle(AdicionarItemCommand request, CancellationToken cancellationToken)
    {
        var compra = await _compraRepository.ObterCompraPorId(request.CompraId);
        var compraItem = new CompraItem(request.ProdutoId, request.Nome, request.Quantidade, request.ValorUnitario);

        if (compra is null)
            throw new InvalidDataException("Compra não encontrada!");

        var compraItemExistente = compra.CompraItemExistente(compraItem);
        compra.AdicionarItem(compraItem);

        if (compraItemExistente)
        {
            _compraRepository.AtualizarItem(
                compra.CompraItens.FirstOrDefault(p => p.ProdutoId == compraItem.ProdutoId));
        }
        else
        {
            _compraRepository.AdicionarItem(compraItem);
        }

        await _compraRepository.UnitOfWork.Commit();
    }

    public async Task Handle(RemoverItemCommand request, CancellationToken cancellationToken)
    {
        var compra = await _compraRepository.ObterCompraPorId(request.CompraId);

        if (compra is null)
            throw new InvalidDataException("Compra não encontrada!");

        var compraItem = await _compraRepository.ObterItemPorCompra(compra.Id, request.ProdutoId);
        compra.RemoverItem(compraItem);

        _compraRepository.RemoverItem(compraItem);
        _compraRepository.Atualizar(compra);

        await _compraRepository.UnitOfWork.Commit();
    }

    public async Task Handle(CancelarCompraCommand request, CancellationToken cancellationToken)
    {
        var compra = await _compraRepository.ObterCompraPorId(request.CompraId);
        compra.CancelarCompra();

        _compraRepository.Atualizar(compra);
        await _compraRepository.UnitOfWork.Commit();
    }

    private Compra MapearCompra(CriarCompraCommand message)
    {
        var compra = new Compra(message.ClienteId, message.ValorTotal,
            message.CompraItens.Select(CompraItemDTO.MapearCompraItem).ToList(),
            message.Filial, message.Desconto);

        return compra;
    }
}
