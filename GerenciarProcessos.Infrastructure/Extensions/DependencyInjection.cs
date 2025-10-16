using GerenciarProcessos.Application.Interfaces;
using GerenciarProcessos.Application.Services;
using GerenciarProcessos.Domain.Interfaces;
using GerenciarProcessos.Infra.Repositories;
using GerenciarProcessos.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciarProcessos.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IProcessoRepository, ProcessoRepository>();
        services.AddScoped<IProcessoService, ProcessoService>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        return services;
    }
}
