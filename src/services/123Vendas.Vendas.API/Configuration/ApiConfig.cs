using _123Vendas.Vendas.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace _123Vendas.Vendas.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddDbContext<VendasContext>(options =>
             options.UseSqlite(configuration.GetConnectionString("VendasDb")));
    }
}
