using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using XPTO.Application.DTOs;

namespace XPTO.Presentation.API.Controllers.v2
{
    [ApiController]
    [ApiVersion("1.1", Deprecated = false)]
    [Route("api/v{version:apiVersion}/Clientes")]
    [Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        public ClienteController()
        {

        }


        [HttpGet(Name = "Listar Todos os clientes", Order = 1)]
        public List<ClienteDTO> GetClientes()
        {
            return new List<ClienteDTO>() {
                new ClienteDTO
                {
                    Id = Guid.NewGuid(),
                    Nome = "Alexandre Vale",
                    Email = "alevale@gmail.com",
                    Telefone = "21980827649",
                    Endereco = null
                }
            };
        }
    }
}
