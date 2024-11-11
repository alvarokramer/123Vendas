using MediatR;

namespace _123Vendas.Vendas.API.Application.Commands;

public class CancelarCompraCommand : IRequest
{
    public Guid CompraId { get; private set; }

    public CancelarCompraCommand(Guid compraId) 
    {
        CompraId = compraId;
    }
}
