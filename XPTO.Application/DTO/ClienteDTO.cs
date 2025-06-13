using System.ComponentModel.DataAnnotations;
using XPTO.Domain.Entities;

namespace XPTO.Application.DTOs
{
    public class ClienteDTO : IDataTransferObject
    {
        [Key]
        public new Guid Id { get; set; } = Guid.Empty;
        public string Nome { get; set; } = string.Empty;
        [System.Text.Json.Serialization.JsonIgnore]
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public EnderecoDTO? Endereco { get; set; } = new EnderecoDTO();
    }
}