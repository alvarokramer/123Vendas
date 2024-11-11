using _123Vendas.Vendas.Domain.DomainObj;
using _123Vendas.Vendas.Domain.Vendas;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.Vendas.Infra.Data.Repository;

public class CompraRepository : ICompraRepository
{
    private readonly ComprasContext _context;   

    public CompraRepository(ComprasContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Compra> ObterCompraPorId(Guid compraId)
    {
        var compra = await _context.Compras.FindAsync(compraId);

        if (compra is not null)
        {
            await _context.Entry(compra)
                .Collection(i => i.CompraItens).LoadAsync();
        }

        return compra;            
    }    

    public void Adicionar(Compra compra)
    {
        _context.Compras.Add(compra);
    }        

    public void Atualizar(Compra compra)
    {
        _context.Compras.Update(compra);
    }

    public async Task<CompraItem> ObterItemPorCompra(Guid compraId, Guid produtoId)
    {
        return await _context.CompraItens.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.CompraId == compraId);
    }

    public void AdicionarItem(CompraItem compraItem)
    {
        _context.CompraItens.Add(compraItem);
    }

    public void AtualizarItem(CompraItem compraItem)
    {
        _context.CompraItens.Update(compraItem);
    }

    public void RemoverItem(CompraItem compraItem)
    {
        _context.CompraItens.Remove(compraItem);
    }

    public void Dispose()
    {
        _context.Dispose();
    }    
}
