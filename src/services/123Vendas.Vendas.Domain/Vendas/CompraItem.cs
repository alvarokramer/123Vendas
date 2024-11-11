using _123Vendas.Vendas.Domain.DomainObj;

namespace _123Vendas.Vendas.Domain.Vendas;

public class CompraItem : Entity
{
    public Guid CompraId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string ProdutoNome { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public decimal ValorTotal { get; private set; }

    // EF Relation
    public Compra Compra { get; set; }

    public CompraItem(Guid produtoId, string produtoNome, int quantidade,
            decimal valorUnitario)
    {
        ProdutoId = produtoId;
        ProdutoNome = produtoNome;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
    }

    // EF constructor
    protected CompraItem() { }

    internal void AssociarCompra(Guid compraId)
    {
        CompraId = compraId;
    }

    internal void AdicionarUnidades(int unidades)
    {
        Quantidade += unidades;
    }

    internal decimal CalcularValor()
    {
        ValorTotal = Quantidade * ValorUnitario;
        return ValorTotal;
    }
}
