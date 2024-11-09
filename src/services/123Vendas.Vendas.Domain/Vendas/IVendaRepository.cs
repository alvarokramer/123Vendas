using _123Vendas.Vendas.Domain.DomainObj;

namespace _123Vendas.Vendas.Domain.Vendas;

public interface IVendaRepository : IRepository<Venda>
{
    void Add(Venda venda);
    void Update(Venda venda);
    Task<Venda> GetAsync(int vendaId);
}
