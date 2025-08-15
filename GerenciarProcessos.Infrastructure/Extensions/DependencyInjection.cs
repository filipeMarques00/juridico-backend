using GerenciarProcessos.Application.Interfaces;
using GerenciarProcessos.Application.Services;
using GerenciarProcessos.Domain.Interfaces;
using GerenciarProcessos.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciarProcessos.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IClienteService, ClienteService>();

        return services;
    }
}
