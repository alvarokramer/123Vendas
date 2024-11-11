using _123Vendas.Vendas.Domain.DomainObj;

namespace _123Vendas.Vendas.Domain.Vendas;

public class Compra : Entity, IAggregateRoot
{
    public Compra(Guid clienteId, decimal valorTotal, List<CompraItem> compraItens, string filial,
        decimal desconto = 0)
    {
        ClienteId = clienteId;
        ValorTotal = valorTotal;
        _compraItens = compraItens;
        Desconto = desconto;
        Filial = filial;
        NumeroCompra = new Random().Next(1, 9999);
        DataCompra = DateTime.Today;
    }

    // EF constructor
    protected Compra() { }
    
    public int NumeroCompra { get; private set; }
    public DateTime DataCompra { get; private set; }
    public Guid ClienteId { get; private set; }
    public decimal Desconto { get; private set; }
    public decimal ValorTotal { get; private set; }
    public string Filial { get; private set; }
    public CompraStatus CompraStatus { get; private set; }

    private readonly List<CompraItem> _compraItens;
    public IReadOnlyCollection<CompraItem> CompraItens => _compraItens;

    public bool CompraItemExistente(CompraItem item)
    {
        return CompraItens.Any(p => p.ProdutoId == item.ProdutoId);
    }

    public void AdicionarItem(CompraItem item)
    {
        item.AssociarCompra(Id);

        if (CompraItemExistente(item))
        {
            var itemExistente = _compraItens.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
            itemExistente.AdicionarUnidades(item.Quantidade);
            item = itemExistente;

            _compraItens.Remove(itemExistente);
        }

        _compraItens.Add(item);
        CalcularValorCompra();
    }

    public void RemoverItem(CompraItem item)
    {
        var itemExistente = CompraItens.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

        if (itemExistente is null) throw new InvalidDataException("O item não pertence a compra");

        _compraItens.Remove(itemExistente);
        CalcularValorCompra();
    }

    public void AutorizarCompra()
    {
        CompraStatus = CompraStatus.Autorizado;
    }

    public void CancelarCompra()
    {
        CompraStatus = CompraStatus.Cancelado;
    }

    public void CalcularValorCompra()
    {
        ValorTotal = CompraItens.Sum(item => item.CalcularValor());
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
