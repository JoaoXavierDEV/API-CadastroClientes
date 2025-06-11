using XPTO.Domain.Entities;

namespace XPTO.Application.DTOs
{
    public class ClienteDTO : IDataTransferObject
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public EnderecoDTO Endereco { get; set; }
    }
}