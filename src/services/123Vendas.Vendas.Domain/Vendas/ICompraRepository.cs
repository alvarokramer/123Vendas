using _123Vendas.Vendas.Domain.DomainObj;

namespace _123Vendas.Vendas.Domain.Vendas;

public interface ICompraRepository : IRepository<Compra>
{
    void Adicionar(Compra compra);
    void Atualizar(Compra compra);
    Task<Compra> ObterCompraPorId(Guid compraId);
    Task<CompraItem> ObterItemPorCompra(Guid compraId, Guid produtoId);
    void AdicionarItem(CompraItem compraItem);
    void AtualizarItem(CompraItem compraItem);
    void RemoverItem(CompraItem compraItem);
}
