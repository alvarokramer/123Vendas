using _123Vendas.Vendas.Domain.Vendas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _123Vendas.Vendas.Infra.Data.Mappings;

public class ProdutoItemMapping : IEntityTypeConfiguration<ProdutoItem>
{
    public void Configure(EntityTypeBuilder<ProdutoItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.ProdutoNome)
            .IsRequired();            

        // 1 : N => Venda : Itens
        builder.HasOne(c => c.Venda)
            .WithMany(c => c.ProdutoItens);

        builder.ToTable("ProdutoItens");
    }
}
