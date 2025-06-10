using XPTO.Domain.Entities;

namespace XPTO.Infrastructure.Data.Context;

public static class DbInitializer
{

    public static readonly List<Cliente> Clientes = new List<Cliente>
    {
        new Cliente("João","joao@outlook.com", "2178985231",new Endereco("Rua D","1024","Cidade", "RJ", "20258987"))
    };



    public static void Initialize(ApplicationDbContext context)
    {

    }
}
