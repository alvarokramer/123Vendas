using _123Vendas.Vendas.Domain.Vendas;
using FluentAssertions;

namespace Vendas.UnitTests.Domain;

public class CompraTests
{

    [Fact(DisplayName = "Criar Nova Compra")]
    [Trait("Categoria", "Vendas - Compra")]
    public void CriarCompra_Deve_Calcular_Total()
    {
        // Arrange
        var resultadoEsperado = 20;
        var itens = new List<CompraItem>() 
        { 
            new CompraItem(Guid.NewGuid(), "Produto Teste 1", 2, 5),
            new CompraItem(Guid.NewGuid(), "Produto Teste 2", 1, 10)
        };

        var compra = new Compra(Guid.NewGuid(), 20, itens, "Filial 1");

        // Act
        compra.CalcularValorCompra();

        // Assert         
        compra.ValorTotal.Should().Be(resultadoEsperado);        
    }

    [Fact(DisplayName = "Adicionar Item Compra")]
    [Trait("Categoria", "Vendas - Compra")]
    public void AdicionarItem_Deve_Recalcular_Total()
    {
        // Arrange
        var resultadoEsperado = 25;
        var itens = new List<CompraItem>() { new CompraItem(Guid.NewGuid(), "Produto Teste 1", 2, 5) };
        var novoItem = new CompraItem(Guid.NewGuid(), "Produto Teste 2", 1, 15);

        var compra = new Compra(Guid.NewGuid(), 10, itens, "Filial 1");

        // Act
        compra.AdicionarItem(novoItem);

        // Assert         
        compra.ValorTotal.Should().Be(resultadoEsperado);
    }

    [Fact(DisplayName = "Remover Item Compra")]
    [Trait("Categoria", "Vendas - Compra")]
    public void RemoverItem_Deve_Remover_E_Recalcular_Total()
    {
        // Arrange
        var resultadoEsperado = 10;
        var itemParaRemover = new CompraItem(Guid.NewGuid(), "Produto Teste 2", 1, 15);
        var itens = new List<CompraItem>() 
        {
            new CompraItem(Guid.NewGuid(), "Produto Teste 1", 2, 5),
            itemParaRemover
        };

        var compra = new Compra(Guid.NewGuid(), 25, itens, "Filial 1");

        // Act
        compra.RemoverItem(itemParaRemover);

        // Assert         
        compra.ValorTotal.Should().Be(resultadoEsperado);
    }
}
