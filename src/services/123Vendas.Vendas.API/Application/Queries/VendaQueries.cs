using _123Vendas.Vendas.API.Application.DTO;
using _123Vendas.Vendas.Domain.Vendas;

namespace _123Vendas.Vendas.API.Application.Queries;

public interface IVendaQueries
{
    Task<VendaDTO> ObterVenda(Guid vendaId);    
}

public class VendaQueries : IVendaQueries
{
    private readonly IVendaRepository _vendaRepository;

    public VendaQueries(IVendaRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public async Task<VendaDTO> ObterVenda(Guid vendaId)
    {
        var venda = await _vendaRepository.ObterVendaPorId(vendaId);

        return VendaDTO.ParaVendaDTO(venda);
    }
}
