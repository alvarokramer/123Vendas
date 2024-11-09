using _123Vendas.Vendas.Domain.Vendas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _123Vendas.Vendas.Infra.Data.Mappings;

public class VendaMapping : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.NroVenda)
                .HasDefaultValueSql("NEXT VALUE FOR SequenciaNroVenda");

        // 1 : N => Venda : ProdutoItens
        builder.HasMany(c => c.ProdutoItens)
                .WithOne(c => c.Venda)
                .HasForeignKey(c => c.VendaId);

        builder.ToTable("Vendas");
    }
}
