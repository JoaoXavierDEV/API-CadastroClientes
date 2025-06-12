using Microsoft.EntityFrameworkCore;
using XPTO.Domain.Entities;

namespace XPTO.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        //{
        //}

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Console.WriteLine("Configuring ApplicationDbContext...");

            //optionsBuilder
            //    .UseInMemoryDatabase("xpto-database")
            //    .EnableSensitiveDataLogging()
            //    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Error)
            //    .UseSeeding((x, _) =>
            //    {
            //        bool hasData = x.Set<Cliente>().Any() || x.Set<Endereco>().Any();

            //        if (!hasData)
            //        {
            //            x.Set<Cliente>().AddRange(DbInitializer.Clientes);
            //            x.SaveChanges();
            //        }
            //    });
        }


        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
    }


}
