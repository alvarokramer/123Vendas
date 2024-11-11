using MediatR;

namespace _123Vendas.Vendas.API.Application.Commands;

public class RemoverItemCommand : IRequest
{
    public Guid CompraId { get; private set; }
    public Guid ProdutoId { get; private set; }

    public RemoverItemCommand(Guid compraId, Guid produtoId)
    {
        CompraId = compraId;
        ProdutoId = produtoId;
    }
}
