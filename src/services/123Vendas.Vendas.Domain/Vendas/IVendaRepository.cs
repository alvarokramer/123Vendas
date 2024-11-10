using _123Vendas.Vendas.Domain.DomainObj;

namespace _123Vendas.Vendas.Domain.Vendas;

public interface IVendaRepository : IRepository<Venda>
{
    void Adicionar(Venda venda);
    void Atualizar(Venda venda);
    Task<Venda> ObterVendaPorId(Guid vendaId);
    Task<VendaItem> ObterItemPorVenda(Guid vendaId, Guid produtoId);
    void RemoverItem(VendaItem vendaItem);
}
