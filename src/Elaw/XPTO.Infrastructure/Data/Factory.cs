using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using XPTO.Infrastructure.Data.Context;

namespace XPTO.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[]? args = null)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables()
                .AddUserSecrets<ApplicationDbContext>()
                .Build();

        var optionsBuilder = new DbContextOptionsBuilder();

        optionsBuilder
            .UseInMemoryDatabase("xpto-database")
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Error);
        //.UseSeeding((x, _) =>
        //{
        //    //bool hasData = x.Set<Cliente>().Any() || x.Set<Endereco>().Any();

        //    //if (!hasData)
        //    //{
        //    //    x.Set<Cliente>().AddRange(DbInitializer.Clientes);
        //    //    x.SaveChanges();
        //    //}
        //});

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}



/*

// criar primeira migração
dotnet ef migrations add InitialCreate --project C:\Projetos\net\HeavyApps\src\HeavyApps.Blog.Infrastructure\HeavyApps.Blog.Infrastructure.csproj --context "HeavyApps.Blog.Infrastructure.Data.Context.ApplicationDbContext" --output-dir Data/Migrations

// criar migração com outro nome
dotnet ef migrations add ImplementarLike --project C:\Projetos\net\HeavyApps\src\HeavyApps.Blog.Infrastructure\HeavyApps.Blog.Infrastructure.csproj --context "HeavyApps.Blog.Infrastructure.Data.Context.ApplicationDbContext" --output-dir Data/Migrations

// atualizar o banco com a nova migração
dotnet ef database update --project C:\Projetos\net\HeavyApps\src\HeavyApps.Blog.Infrastructure\HeavyApps.Blog.Infrastructure.csproj --context "HeavyApps.Blog.Infrastructure.Data.Context.ApplicationDbContext"

// resea o banco de dados
dotnet ef database drop --force --project C:\Projetos\net\HeavyApps\src\HeavyApps.Blog.Infrastructure\HeavyApps.Blog.Infrastructure.csproj --context HeavyApps.Blog.Infrastructure.Data.Context.ApplicationDbContext

// apaga a pasta migraions
//rmdir -rf C:\Projetos\net\HeavyApps\src\HeavyApps.Blog.Infrastructure\Data\Migrations

rd /q /s C:\Projetos\net\HeavyApps\src\HeavyApps.Blog.Infrastructure\Data\Migrations
*/