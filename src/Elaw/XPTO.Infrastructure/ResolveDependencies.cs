using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XPTO.Domain.Entities;
using XPTO.Domain.Interfaces;
using XPTO.Infrastructure.Data.Context;
using XPTO.Infrastructure.Data.Repositories;

namespace XPTO.Infrastructure
{
    public static class ResolveDependencies
    {
        public static void AddLayerInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("xpto-database")
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Error);
                options.UseSeeding((x, _) =>
                {
                    var existeDados = x.Set<Cliente>().Any();

                    if (!existeDados)
                    {
                        Console.WriteLine("Base Clientes Criada...");

                        x.Set<Cliente>().AddRange(DbInitializer.DadosClientes());
                        x.SaveChanges();
                    }


                });
            });
        }
    }
}
