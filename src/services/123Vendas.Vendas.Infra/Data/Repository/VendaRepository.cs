using _123Vendas.Vendas.Domain.DomainObj;
using _123Vendas.Vendas.Domain.Vendas;

namespace _123Vendas.Vendas.Infra.Data.Repository;

public class VendaRepository : IVendaRepository
{
    private readonly VendasContext _context;   

    public VendaRepository(VendasContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Venda> GetAsync(int vendaId)
    {
        var venda = await _context.Vendas.FindAsync(vendaId);

        if (venda is not null)
        {
            await _context.Entry(venda)
                .Collection(i => i.ProdutoItens).LoadAsync();
        }

        return venda;            
    }

    public void Add(Venda venda)
    {
        _context.Vendas.Add(venda);
    }        

    public void Update(Venda venda)
    {
        _context.Vendas.Update(venda);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
