using _123Vendas.Vendas.Domain.Vendas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _123Vendas.Vendas.Infra.Data.Mappings;

public class VendaItemMapping : IEntityTypeConfiguration<VendaItem>
{
    public void Configure(EntityTypeBuilder<VendaItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.ProdutoNome)
            .IsRequired();            

        // 1 : N => Venda : Itens
        builder.HasOne(c => c.Venda)
            .WithMany(c => c.VendaItens);

        builder.ToTable("VendaItens");
    }
}
