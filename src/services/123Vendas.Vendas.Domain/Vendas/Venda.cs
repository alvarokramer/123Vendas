using _123Vendas.Vendas.Domain.DomainObj;

namespace _123Vendas.Vendas.Domain.Vendas;

public class Venda : Entity, IAggregateRoot
{
    public Venda(Guid clienteId, decimal valorTotal, List<VendaItem> vendaItens, string filial,
        decimal desconto = 0)
    {
        ClienteId = clienteId;
        ValorTotal = valorTotal;
        _vendaItens = vendaItens;
        Desconto = desconto;
        Filial = filial;
        NumeroVenda = new Random().Next(1, 9999);
        DataVenda = DateTime.Today;
    }

    // EF constructor
    protected Venda() { }
    
    public int NumeroVenda { get; private set; }
    public DateTime DataVenda { get; private set; }
    public Guid ClienteId { get; private set; }
    public decimal Desconto { get; private set; }
    public decimal ValorTotal { get; private set; }
    public string Filial { get; private set; }
    public VendaStatus VendaStatus { get; private set; }

    private readonly List<VendaItem> _vendaItens;
    public IReadOnlyCollection<VendaItem> VendaItens => _vendaItens;

    public bool VendaItemExistente(VendaItem item)
    {
        return VendaItens.Any(p => p.ProdutoId == item.ProdutoId);
    }

    public void RemoverItem(VendaItem item)
    {
        var itemExistente = VendaItens.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

        if (itemExistente is null) throw new InvalidDataException("O item não pertence a venda");

        _vendaItens.Remove(itemExistente);
        CalcularValorVenda();
    }

    public void AutorizarVenda()
    {
        VendaStatus = VendaStatus.Autorizado;
    }

    public void CancelarVenda()
    {
        VendaStatus = VendaStatus.Cancelado;
    }

    public void CalcularValorVenda()
    {
        ValorTotal = VendaItens.Sum(item => item.CalcularValor());
        CalcularValorTotalDesconto();
    }

    private void CalcularValorTotalDesconto()
    {
        decimal desconto = Desconto < 0 ? 0 : Desconto;
        var valor = ValorTotal;

        valor -= desconto;

        ValorTotal = valor < 0 ? 0 : valor;
        Desconto = desconto;
    }
}
