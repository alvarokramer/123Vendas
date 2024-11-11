using _123Vendas.Vendas.Domain.Vendas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _123Vendas.Vendas.Infra.Data.Mappings;

public class CompraItemMapping : IEntityTypeConfiguration<CompraItem>
{
    public void Configure(EntityTypeBuilder<CompraItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.ProdutoNome)
            .IsRequired();            

        // 1 : N => Compra : Itens
        builder.HasOne(c => c.Compra)
            .WithMany(c => c.CompraItens);

        builder.ToTable("CompraItens");
    }
}
