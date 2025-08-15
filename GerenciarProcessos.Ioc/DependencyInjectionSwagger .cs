using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GerenciarProcessos.InfraIoc
{
    public static class DependencyInjectionSwagger
    {
        public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // ✅ ESSENCIAL: Define a versão da especificação
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "GerenciarProcessos API",
                    Version = "v1"
                });

                // ✅ Define esquema de autenticação via JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT assim: Bearer {seu token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // ✅ Aplica o esquema de segurança globalmente
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }
    }
}
