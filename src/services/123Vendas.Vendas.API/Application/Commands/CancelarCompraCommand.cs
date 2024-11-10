using MediatR;

namespace _123Vendas.Vendas.API.Application.Commands;

public class CancelarCompraCommand : IRequest
{
    public Guid VendaId { get; private set; }

    public CancelarCompraCommand(Guid vendaId) 
    {
        VendaId = vendaId;
    }
}
