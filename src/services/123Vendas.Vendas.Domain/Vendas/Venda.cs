using _123Vendas.Vendas.Domain.DomainObj;

namespace _123Vendas.Vendas.Domain.Vendas;

public class Venda : Entity, IAggregateRoot
{
    public Venda(Guid clienteId, decimal valorTotal, List<ProdutoItem> vendaItens, string filial,
        decimal desconto = 0)
    {
        ClienteId = clienteId;
        ValorTotal = valorTotal;
        _vendaItens = vendaItens;

        Desconto = desconto;
        Filial = filial;
    }

    // EF constructor
    protected Venda() { }

    public int NroVenda { get; private set; }
    public DateTime DataVenda { get; private set; }
    public Guid ClienteId { get; private set; }
    public decimal Desconto { get; private set; }
    public decimal ValorTotal { get; private set; }
    public string Filial { get; private set; }
    public VendaStatus VendaStatus { get; private set; }

    private readonly List<ProdutoItem> _vendaItens;
    public IReadOnlyCollection<ProdutoItem> ProdutoItens => _vendaItens;

    public void AutorizarVenda()
    {
        VendaStatus = VendaStatus.Autorizado;
    }

    public void CalcularValorVenda()
    {
        ValorTotal = ProdutoItens.Sum(p => p.CalcularValor());
        CalcularValorTotalDesconto();
    }

    public void CalcularValorTotalDesconto()
    {
        decimal desconto = Desconto < 0 ? 0 : Desconto;
        var valor = ValorTotal;

        valor -= desconto;

        ValorTotal = valor < 0 ? 0 : valor;
        Desconto = desconto;
    }
}
