using _123Vendas.Vendas.Domain.DomainObj;
using _123Vendas.Vendas.Domain.Vendas;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.Vendas.Infra.Data.Repository;

public class VendaRepository : IVendaRepository
{
    private readonly VendasContext _context;   

    public VendaRepository(VendasContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Venda> ObterVendaPorId(Guid vendaId)
    {
        var venda = await _context.Vendas.FindAsync(vendaId);

        if (venda is not null)
        {
            await _context.Entry(venda)
                .Collection(i => i.VendaItens).LoadAsync();
        }

        return venda;            
    }    

    public void Adicionar(Venda venda)
    {
        _context.Vendas.Add(venda);
    }        

    public void Atualizar(Venda venda)
    {
        _context.Vendas.Update(venda);
    }

    public async Task<VendaItem> ObterItemPorVenda(Guid vendaId, Guid produtoId)
    {
        return await _context.VendaItens.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.VendaId == vendaId);
    }

    public void RemoverItem(VendaItem vendaItem)
    {
        _context.VendaItens.Remove(vendaItem);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
