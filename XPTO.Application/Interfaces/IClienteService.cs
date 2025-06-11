using XPTO.Application.DTOs;

namespace XPTO.Application.Interfaces
{
    public interface IClienteService
    {
        IEnumerable<ClienteDTO> ObterTodosClientes();
        ClienteDTO ObterPorId(Guid id);
        void Adicionar(ClienteDTO dto);
        void Atualizar(ClienteDTO dto);
        void Deletar(Guid id);
    }
}

