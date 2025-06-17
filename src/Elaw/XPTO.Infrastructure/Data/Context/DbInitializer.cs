using XPTO.Domain.Entities;

namespace XPTO.Infrastructure.Data.Context;

public static class DbInitializer
{

    public static readonly List<Cliente> Clientes = new List<Cliente>
    {
        new Cliente("João","joao@outlook.com", "2178985231",
                    new Endereco("Rua D","1024","Cidade", "RJ", "20258-987") {Id = Guid.Parse("962AE9D1-A200-4FBF-81AA-72837C092B67")})
        {
            Id = Guid.Parse("ac0ff7b5-85fc-43fc-8e39-a7d5f5910582")
        },
        new Cliente("Pedro","pedro@gmail.com", "2199998878") { Id = Guid.Parse("07ad45c3-2328-41b7-9bb2-b97a49c38b2b") },
    };

    public static readonly List<Endereco> Enderecos = new List<Endereco>
    {
        new Endereco("Av Getúlio de Moura","1024","Nova Iguaçu", "RJ", "20258-987"),
        new Endereco("Rua Minas Gerais","36","Mesquita", "RJ", "22333-666")
    };

    public static List<Cliente> DadosClientes()
    {
        var endereco = new Endereco("Rua D", "1024", "Cidade", "RJ", "20258-987") { Id = Guid.Parse("962AE9D1-A200-4FBF-81AA-72837C092B67") };
        return
        new List<Cliente>
        {
            new Cliente("João","joao@outlook.com", "21 97898 5231", endereco)
            {
                Id = Guid.Parse("ac0ff7b5-85fc-43fc-8e39-a7d5f5910582")
            },
            new Cliente("Pedro Augusto","pedro@gmail.com", "24 99999 8878") { Id = Guid.Parse("07ad45c3-2328-41b7-9bb2-b97a49c38b2b") },
            new Cliente("Arthur Pereira","ArthurPereira@gmail.com", "31 99512 2365") { Id = Guid.Parse("323799cb-54e0-4804-8076-b2ab1f4a152d") }
        };
    }


}
