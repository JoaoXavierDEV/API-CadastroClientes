using System.ComponentModel.DataAnnotations;
using XPTO.Domain.Entities;

namespace XPTO.Application.DTO
{
    public sealed record ClienteDTO : IDataTransferObject
    {
        [Key]
        public Guid Id { get; set; } = Guid.Empty;
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Email é obrigatório.")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Telefone { get; set; } = string.Empty;
        public EnderecoDTO? Endereco { get; set; } = new EnderecoDTO();

        public ClienteDTO()
        {
            
        }

        public ClienteDTO(string nome, string email, string telefone)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }

        public ClienteDTO(string nome, string email, string telefone, EnderecoDTO? endereco)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }
    }
}