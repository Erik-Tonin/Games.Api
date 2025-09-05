using FIAP.Games.Application.Contracts.IApplicationService;
using FIAP.Games.Application.Implementations;
using FIAP.Games.Domain.Contracts.IRepositories;
using FIAP.Games.Infra.Context;
using FIAP.Games.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Games.Crosscutting
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Corrigido para adicionar apenas uma configuração do DbContext
            services.AddDbContext<MicroServiceContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("FiapConnection")));

            // Configuração de CORS
            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            // Registro dos serviços de aplicação
            services.AddScoped<IGameApplicationService, GameApplicationService>();
            services.AddScoped<IUserLibraryApplicationService, UserLibraryApplicationService>();

            // Registro dos repositórios
            services.AddScoped<IGameRepository, GameRepositorie>();
            services.AddScoped<IUserLibraryRepository, UserLibraryRepositorie>();

            return services;
        }
    }
}
