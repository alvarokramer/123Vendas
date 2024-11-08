namespace _123Vendas.Vendas.Domain.Vendas;

public class ProdutoItem
{
    public Guid VendaId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public string ProdutoNome { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }

    internal decimal CalcularValor()
    {
        return Quantidade * ValorUnitario;
    }
}
