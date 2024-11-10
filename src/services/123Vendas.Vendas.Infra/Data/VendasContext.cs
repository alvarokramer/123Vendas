using _123Vendas.Vendas.Domain.DomainObj;
using _123Vendas.Vendas.Domain.Vendas;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.Vendas.Infra.Data;

public class VendasContext : DbContext, IUnitOfWork
{
    public VendasContext(DbContextOptions<VendasContext> options)
        : base(options)
    {        
    }

    public DbSet<Venda> Vendas { get; set; }
    public DbSet<VendaItem> VendaItens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendasContext).Assembly);        

        base.OnModelCreating(modelBuilder);
    }    

    public async Task<bool> Commit(CancellationToken cancellationToken = default)
    {
        var sucesso = await base.SaveChangesAsync() > 0;

        return sucesso;
    }
}
