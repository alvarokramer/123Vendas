using _123Vendas.Vendas.API.Application.DTO;
using _123Vendas.Vendas.Domain.Vendas;

namespace _123Vendas.Vendas.API.Application.Queries;

public interface ICompraQueries
{
    Task<CompraDTO> ObterCompra(Guid compraId);
}

public class CompraQueries : ICompraQueries
{
    private readonly ICompraRepository _compraRepository;

    public CompraQueries(ICompraRepository compraRepository)
    {
        _compraRepository = compraRepository;
    }

    public async Task<CompraDTO> ObterCompra(Guid compraId)
    {
        var compra = await _compraRepository.ObterCompraPorId(compraId);

        return CompraDTO.MapearParaCompraDTO(compra);
    }
}
