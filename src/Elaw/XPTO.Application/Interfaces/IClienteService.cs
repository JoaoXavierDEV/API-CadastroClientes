using XPTO.Application.DTO;

namespace XPTO.Application.Interfaces
{
    public interface IClienteService
    {
        IEnumerable<ClienteDTO> ObterTodosClientes();
        ClienteDTO ObterPorId(Guid id);
        Task Adicionar(ClienteDTO dto);
        //Task Atualizar(ClienteDTO dto);
        void Atualizar(ClienteDTO dto);
        void Deletar(Guid id);
    }
}

