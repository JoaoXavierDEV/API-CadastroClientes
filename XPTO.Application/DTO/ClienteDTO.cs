using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XPTO.Domain.Entities;

namespace XPTO.Application.DTOs
{
    public class ClienteDTO : IDataTransferObject
    {
        public ClienteDTO()
        {
        }

        [Key]

        public new Guid Id { get; set; } = Guid.Empty;

        //[Required(ErrorMessage = "O Nome é Obrigatório")]
        //[StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        [NotMapped]

        public EnderecoDTO? Endereco { get; set; } = new EnderecoDTO();
    }
}