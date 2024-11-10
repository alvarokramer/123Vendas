using _123Vendas.Vendas.API.Application.Commands;
using _123Vendas.Vendas.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace _123Vendas.Vendas.API.Controllers;

public class VendaController : Controller
{
    private readonly IMediator _mediator;
    private readonly IVendaQueries _vendaQueries;

    public VendaController(IMediator mediator, IVendaQueries vendaQueries)
    {
        _mediator = mediator;
        _vendaQueries = vendaQueries;
    }

    [HttpPost]
    [Route("compra")]
    public async Task<IActionResult> CriarCompra([FromBody] CriarCompraCommand compraCommand)
    {
        await _mediator.Send(compraCommand);

        return Ok();
    }

    [HttpGet]
    [Route("compra/{compraId:guid}")]
    public async Task<IActionResult> ObterCompra([FromRoute] Guid compraId)
    {
        var venda = await _vendaQueries.ObterVenda(compraId);

        return venda is null ? NotFound() : Ok(venda);
    }
}
