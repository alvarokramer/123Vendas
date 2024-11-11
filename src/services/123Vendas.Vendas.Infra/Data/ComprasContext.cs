using _123Vendas.Vendas.Domain.DomainObj;
using _123Vendas.Vendas.Domain.Vendas;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.Vendas.Infra.Data;

public class ComprasContext : DbContext, IUnitOfWork
{
    public ComprasContext(DbContextOptions<ComprasContext> options)
        : base(options)
    {        
    }

    public DbSet<Compra> Compras { get; set; }
    public DbSet<CompraItem> CompraItens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComprasContext).Assembly);        

        base.OnModelCreating(modelBuilder);
    }    

    public async Task<bool> Commit(CancellationToken cancellationToken = default)
    {
        var sucesso = await base.SaveChangesAsync() > 0;

        return sucesso;
    }
}
