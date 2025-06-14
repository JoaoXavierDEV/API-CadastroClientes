using XPTO.Domain.Entities;

namespace XPTO.Domain.Interfaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        IEnumerable<Endereco> ObterTodosEnderecos();
    }
}
