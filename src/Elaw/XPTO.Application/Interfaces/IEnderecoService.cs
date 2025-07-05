using XPTO.Application.DTO;

namespace XPTO.Application.Interfaces
{
    internal interface IEnderecoService
    {
        IEnumerable<EnderecoDTO> ObterTodosEnderecos();
        EnderecoDTO GetById(Guid id);
        void Add(EnderecoDTO enderecoDto);
        void Update(EnderecoDTO enderecoDto);
        void Remove(Guid id);
        void Remove(EnderecoDTO dto);
    }
}
