using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XPTO.Domain.Entities;
using XPTO.Domain.Interfaces;
using XPTO.Infrastructure.Data.Context;
using XPTO.Infrastructure.Repository;

namespace XPTO.Infrastructure
{
    public static class ResolveDependencies
    {
        public static void AddLayerInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            // Register domain repositories
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            //// Register infrastructure context
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer("YourConnectionStringHere"));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                Console.WriteLine("Configuring ApplicationDbContext...");

                options.UseInMemoryDatabase("xpto-database");
                options.UseSeeding((x, _) =>
                {
                    var existeDados = x.Set<Cliente>().Any();

                    var yy = x.Set<Cliente>().ToList();
                    var tt = x.Set<Endereco>().ToList();

                    if (!existeDados)
                    {
                        x.Set<Cliente>().AddRange(DbInitializer.Clientes);
                        x.SaveChanges();
                    }

                    if (!x.Set<Endereco>().Any())
                    {
                        x.Set<Endereco>().AddRange(DbInitializer.Enderecos);
                        x.SaveChanges();
                    }

                });
            });
        }
    }
}
