using _123Vendas.Vendas.API.Application.Commands;
using _123Vendas.Vendas.API.Application.Queries;
using _123Vendas.Vendas.Domain.Vendas;
using _123Vendas.Vendas.Infra.Data;
using _123Vendas.Vendas.Infra.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace _123Vendas.Vendas.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddDbContext<VendasContext>(options =>
             options.UseSqlite(configuration.GetConnectionString("VendasDb")));
        
        services.AddScoped<IRequestHandler<CriarCompraCommand>, CriarCompraCommandHandler>();
        services.AddScoped<IVendaQueries, VendaQueries>();

        // Data
        services.AddScoped<IVendaRepository, VendaRepository>();        
    }
}
