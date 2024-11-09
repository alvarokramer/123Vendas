using _123Vendas.Vendas.Domain.DomainObj;

namespace _123Vendas.Vendas.Domain.Vendas;

public class ProdutoItem : Entity
{
    public Guid VendaId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string ProdutoNome { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }

    // EF Relation
    public Venda Venda { get; set; }

    public ProdutoItem(Guid produtoId, string produtoNome, int quantidade,
            decimal valorUnitario)
    {
        ProdutoId = produtoId;
        ProdutoNome = produtoNome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    // EF constructor
    protected ProdutoItem() { }

    internal decimal CalcularValor()
    {
        return Quantidade * ValorUnitario;
    }
}
