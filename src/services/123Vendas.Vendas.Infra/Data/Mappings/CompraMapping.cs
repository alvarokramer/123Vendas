using _123Vendas.Vendas.Domain.Vendas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _123Vendas.Vendas.Infra.Data.Mappings;

public class CompraMapping : IEntityTypeConfiguration<Compra>
{
    public void Configure(EntityTypeBuilder<Compra> builder)
    {
        builder.HasKey(c => c.Id);        

        // 1 : N => Compra : CompraItens
        builder.HasMany(c => c.CompraItens)
                .WithOne(c => c.Compra)
                .HasForeignKey(c => c.CompraId);

        builder.ToTable("Compras");
    }
}
