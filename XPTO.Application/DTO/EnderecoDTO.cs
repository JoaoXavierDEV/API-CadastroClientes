using XPTO.Domain.Entities;

namespace XPTO.Application.DTOs;
public class EnderecoDTO : IDataTransferObject
{
    public Guid Id { get; set; }
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Cep { get; set; }

}
