using MediatR;

namespace _123Vendas.Vendas.API.Application.Commands;

public class RemoverItemCommand : IRequest
{
    public Guid VendaId { get; private set; }
    public Guid ProdutoId { get; private set; }

    public RemoverItemCommand(Guid vendaId, Guid produtoId)
    {
        VendaId = vendaId;
        ProdutoId = produtoId;
    }
}
