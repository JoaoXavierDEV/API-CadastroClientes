using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using XPTO.Domain.Entities;

namespace XPTO.Application.DTOs
{
    [JsonSerializable(typeof(ClienteDTO))]
    public class ClienteDTO : IDataTransferObject
    {
        [Key]
        public new Guid Id { get; set; } = Guid.Empty;
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Telefone { get; set; } = string.Empty;
        public EnderecoDTO? Endereco { get; set; } = new EnderecoDTO();
    }
}