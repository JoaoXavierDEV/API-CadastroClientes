using System.ComponentModel.DataAnnotations;
using XPTO.Domain.Entities;

namespace XPTO.Application.DTO;
public sealed record EnderecoDTO : IDataTransferObject
{
    public EnderecoDTO(string rua, string numero, string cidade, string estado, string cep)
    {
        Rua = rua;
        Numero = numero;
        Cidade = cidade;
        Estado = estado;
        Cep = cep;
    }

    public EnderecoDTO()
    {
        
    }

    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Rua { get; set; } = string.Empty;
    [Required]
    public string Numero { get; set; } = string.Empty;
    [Required]
    public string Cidade { get; set; } = string.Empty;
    [Required]
    public string Estado { get; set; } = string.Empty;
    [Required]
    public string Cep { get; set; } = string.Empty;

}
