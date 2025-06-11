namespace XPTO.Application.DTOs
{
	/// <summary>
	/// Data Transfer Object for Cliente.
	/// </summary>
	public class ClienteDTO
	{
		public Guid Id { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Telefone { get; set; }
		public EnderecoDTO Endereco { get; set; }
	}
}