using System.ComponentModel.DataAnnotations;
using XPTO.Domain.Entities;

namespace XPTO.Application.DTOs;
public sealed record EnderecoDTO : IDataTransferObject
{
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
