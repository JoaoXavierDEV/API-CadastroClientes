using XPTO.Domain.Entities;

namespace XPTO.Infrastructure.Data.Context;

public static class DbInitializer
{

    public static readonly List<Cliente> Clientes = new List<Cliente>
    {
        new Cliente("João","joao@outlook.com", "2178985231",new Endereco("Rua D","1024","Cidade", "RJ", "20258987")) { Id = Guid.Parse("ac0ff7b5-85fc-43fc-8e39-a7d5f5910582") },
        new Cliente("Pedro","pedro@gmail.com", "2199998878",new Endereco("Rua D","1024","Cidade", "RJ", "20258987"))
    };


}
