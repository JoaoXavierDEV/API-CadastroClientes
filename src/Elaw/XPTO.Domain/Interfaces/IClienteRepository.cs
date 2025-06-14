using XPTO.Domain.Entities;

namespace XPTO.Domain.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        IEnumerable<Cliente> ObterTodosClientes();
    }
}
