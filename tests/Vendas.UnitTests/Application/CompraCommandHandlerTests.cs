using _123Vendas.Vendas.API.Application.Commands;
using _123Vendas.Vendas.Domain.Vendas;
using Bogus.DataSets;
using Bogus;
using MediatR;
using NSubstitute;
using _123Vendas.Vendas.API.Application.DTO;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Vendas.UnitTests.Application;

public class CompraCommandHandlerTests
{
    private readonly ICompraRepository _compraRepositoryMock;

    public CompraCommandHandlerTests()
    {
        _compraRepositoryMock = Substitute.For<ICompraRepository>();        
    }

    [Fact(DisplayName = "Criar Nova Compra Command")]
    [Trait("Categoria", "Vendas - Compra")]
    public async Task CommandHandler_CriarCompra_Com_Sucesso()
    {
        // Assert
        var fakeCriarCompra = CriarCompraCommandMock(2);

        _compraRepositoryMock.Adicionar(Arg.Any<Compra>());
        _compraRepositoryMock.UnitOfWork.Commit().Returns(Task.FromResult(true));

        // Act
        var handler = new CompraCommandHandler(_compraRepositoryMock);
        var cltToken = new CancellationToken();
        await handler.Handle(fakeCriarCompra, cltToken);

        // Assert
        _compraRepositoryMock.Received().Adicionar(Arg.Any<Compra>());
        await _compraRepositoryMock.Received().UnitOfWork.Received().Commit(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Adicionar Item Command")]
    [Trait("Categoria", "Vendas - Compra")]
    public async Task CommandHandler_AdicionarItem_Com_Sucesso()
    {
        // Assert
        var fakeAdicionarItem = new AdicionarItemCommand(Guid.NewGuid(), 
            Guid.NewGuid(),
            new Faker().Company.CompanyName(),
            new Faker().Random.Int(min: 1, max: 3),
            new Faker().Random.Decimal(min: 1, max: 5));
        
        var fakeCompra = new Compra(Guid.NewGuid(),
            new Faker().Random.Decimal(min: 1, max: 5),
            new List<CompraItem>(),
            new Faker().Company.CompanyName());

        _compraRepositoryMock.ObterCompraPorId(Arg.Any<Guid>()).Returns(Task.FromResult(fakeCompra));
        _compraRepositoryMock.AdicionarItem(Arg.Any<CompraItem>());
        _compraRepositoryMock.UnitOfWork.Commit().Returns(Task.FromResult(true));

        // Act
        var handler = new CompraCommandHandler(_compraRepositoryMock);
        var cltToken = new CancellationToken();
        await handler.Handle(fakeAdicionarItem, cltToken);

        // Assert
        _compraRepositoryMock.Received().AdicionarItem(Arg.Any<CompraItem>());
        await _compraRepositoryMock.Received().UnitOfWork.Received().Commit(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Remover Item Command")]
    [Trait("Categoria", "Vendas - Compra")]
    public async Task CommandHandler_RemoverItem_Com_Sucesso()
    {
        // Assert
        var fakeRemoverItem = new RemoverItemCommand(Guid.NewGuid(), Guid.NewGuid());
        var fakeCompraItem = new CompraItem(Guid.NewGuid(), new Faker().Commerce.Product(), 1, 5);

        var fakeCompra = new Compra(Guid.NewGuid(),
            new Faker().Random.Decimal(min: 1, max: 5),
            new List<CompraItem> { fakeCompraItem },
            new Faker().Company.CompanyName());

        _compraRepositoryMock.ObterCompraPorId(Arg.Any<Guid>()).Returns(Task.FromResult(fakeCompra));
        _compraRepositoryMock.ObterItemPorCompra(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(Task.FromResult(fakeCompraItem));
        _compraRepositoryMock.RemoverItem(Arg.Any<CompraItem>());
        _compraRepositoryMock.Atualizar(Arg.Any<Compra>());
        _compraRepositoryMock.UnitOfWork.Commit().Returns(Task.FromResult(true));

        // Act
        var handler = new CompraCommandHandler(_compraRepositoryMock);
        var cltToken = new CancellationToken();
        await handler.Handle(fakeRemoverItem, cltToken);

        // Assert
        _compraRepositoryMock.Received().RemoverItem(Arg.Any<CompraItem>());
        await _compraRepositoryMock.Received().UnitOfWork.Received().Commit(Arg.Any<CancellationToken>());
    }

    private CriarCompraCommand CriarCompraCommandMock(int quantidadeItens) 
    {
        return new CriarCompraCommand
        {
            ClienteId = Guid.NewGuid(),
            ValorTotal = 10,
            Filial = new Faker().Company.CompanyName(),
            Desconto = decimal.Parse(new Faker().Commerce.Price(min: 1, max: 5)),
            CompraItens = GerarItensCompra(quantidadeItens)
        };
    }

    private List<CompraItemDTO> GerarItensCompra(int quantidade)
    {
        var itens = new Faker<CompraItemDTO>()
            .CustomInstantiator(item => new CompraItemDTO
            {
                ProdutoId = Guid.NewGuid(),
                Nome = item.Commerce.Product(),
                Quantidade = item.Random.Int(min: 1, max: 3),
                ValorUnitario = new Faker().Random.Decimal(min: 1, max: 5)
            });

        return itens.Generate(quantidade);
    }
}
