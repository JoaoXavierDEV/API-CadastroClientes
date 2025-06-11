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
                options.UseInMemoryDatabase("xpto-database");
                options.UseSeeding((x, _) =>
                {
                    var existeDados = x.Set<Cliente>().Any();

                    if (!existeDados)
                    {
                        x.Set<Cliente>().AddRange(DbInitializer.Clientes);
                        x.SaveChanges();
                    }

                });
            });
        }
    }
}
