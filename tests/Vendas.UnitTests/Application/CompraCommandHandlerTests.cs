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
    private readonly IVendaRepository _vendaRepositoryMock;

    public CompraCommandHandlerTests()
    {
        _vendaRepositoryMock = Substitute.For<IVendaRepository>();        
    }

    [Fact(DisplayName = "Criar Nova Compra Command")]
    [Trait("Categoria", "Vendas - Compra")]
    public async Task CommandHandler_CriarCompra_Com_Sucesso()
    {
        // Assert
        var fakeCriarCompra = CriarCompraCommandMock(2);

        _vendaRepositoryMock.Adicionar(Arg.Any<Venda>());
        _vendaRepositoryMock.UnitOfWork.Commit().Returns(Task.FromResult(true));

        // Act
        var handler = new CompraCommandHandler(_vendaRepositoryMock);
        var cltToken = new CancellationToken();
        await handler.Handle(fakeCriarCompra, cltToken);

        // Assert
        _vendaRepositoryMock.Received().Adicionar(Arg.Any<Venda>());
        await _vendaRepositoryMock.Received().UnitOfWork.Received().Commit(Arg.Any<CancellationToken>());
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
        
        var fakeVenda = new Venda(Guid.NewGuid(),
            new Faker().Random.Decimal(min: 1, max: 5),
            new List<VendaItem>(),
            new Faker().Company.CompanyName());

        _vendaRepositoryMock.ObterVendaPorId(Arg.Any<Guid>()).Returns(Task.FromResult(fakeVenda));
        _vendaRepositoryMock.AdicionarItem(Arg.Any<VendaItem>());
        _vendaRepositoryMock.UnitOfWork.Commit().Returns(Task.FromResult(true));

        // Act
        var handler = new CompraCommandHandler(_vendaRepositoryMock);
        var cltToken = new CancellationToken();
        await handler.Handle(fakeAdicionarItem, cltToken);

        // Assert
        _vendaRepositoryMock.Received().AdicionarItem(Arg.Any<VendaItem>());
        await _vendaRepositoryMock.Received().UnitOfWork.Received().Commit(Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Remover Item Command")]
    [Trait("Categoria", "Vendas - Compra")]
    public async Task CommandHandler_RemoverItem_Com_Sucesso()
    {
        // Assert
        var fakeRemoverItem = new RemoverItemCommand(Guid.NewGuid(), Guid.NewGuid());
        var fakeVendaItem = new VendaItem(Guid.NewGuid(), new Faker().Commerce.Product(), 1, 5);

        var fakeVenda = new Venda(Guid.NewGuid(),
            new Faker().Random.Decimal(min: 1, max: 5),
            new List<VendaItem> { fakeVendaItem },
            new Faker().Company.CompanyName());

        _vendaRepositoryMock.ObterVendaPorId(Arg.Any<Guid>()).Returns(Task.FromResult(fakeVenda));
        _vendaRepositoryMock.ObterItemPorVenda(Arg.Any<Guid>(), Arg.Any<Guid>()).Returns(Task.FromResult(fakeVendaItem));
        _vendaRepositoryMock.RemoverItem(Arg.Any<VendaItem>());
        _vendaRepositoryMock.Atualizar(Arg.Any<Venda>());
        _vendaRepositoryMock.UnitOfWork.Commit().Returns(Task.FromResult(true));

        // Act
        var handler = new CompraCommandHandler(_vendaRepositoryMock);
        var cltToken = new CancellationToken();
        await handler.Handle(fakeRemoverItem, cltToken);

        // Assert
        _vendaRepositoryMock.Received().RemoverItem(Arg.Any<VendaItem>());
        await _vendaRepositoryMock.Received().UnitOfWork.Received().Commit(Arg.Any<CancellationToken>());
    }

    private CriarCompraCommand CriarCompraCommandMock(int quantidadeItens) 
    {
        return new CriarCompraCommand
        {
            ClienteId = Guid.NewGuid(),
            ValorTotal = 10,
            Filial = new Faker().Company.CompanyName(),
            Desconto = decimal.Parse(new Faker().Commerce.Price(min: 1, max: 5)),
            VendaItens = GerarItensVenda(quantidadeItens)
        };
    }

    private List<VendaItemDTO> GerarItensVenda(int quantidade)
    {
        var itens = new Faker<VendaItemDTO>()
            .CustomInstantiator(item => new VendaItemDTO
            {
                ProdutoId = Guid.NewGuid(),
                Nome = item.Commerce.Product(),
                Quantidade = item.Random.Int(min: 1, max: 3),
                ValorUnitario = new Faker().Random.Decimal(min: 1, max: 5)
            });

        return itens.Generate(quantidade);
    }
}
