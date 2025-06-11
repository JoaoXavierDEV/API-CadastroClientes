using Microsoft.AspNetCore.Mvc;
using XPTO.Application.DTOs;
using XPTO.Application.Interfaces;
using XPTO.Domain.Entities;
using XPTO.Domain.Interfaces;

namespace XPTO.Presentation.API.Controllers
{
    [ApiController]
    [Route("Clientes")]
    [Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteService _clienteService;
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(ILogger<ClienteController> logger, IClienteService clienteService, IClienteRepository clienteRepository)
        {
            _logger = logger;
            _clienteService = clienteService;
            _clienteRepository = clienteRepository;
        }

        [HttpGet(Name = "Listar Todos os clientes", Order = 1)]
        public IEnumerable<Cliente> GetClientes()
        {
            return _clienteRepository.ObterTodosClientes();
        }


        [HttpGet("/{id:guid}", Name = "Obter um cliente por ID", Order = 2)]
        public async Task<Cliente> ObterClientePorID(Guid id)
        {
            return await _clienteRepository.ObterPorId(id);
        }

        [HttpPost(Name = "Criar um novo cliente", Order = 3)]
        public void CriarCliente(ClienteDTO dto)
        {
            _clienteService.Adicionar(dto);
        }

        [HttpPut("/{id:guid}", Name = "Atualizar um cliente existente", Order = 4)]
        public void AtualizarCliente(System.Guid id, ClienteDTO dto)
        {
            dto.Id = id;
            _clienteService.Atualizar(dto);
        }

        [HttpDelete("/{id:guid}", Name = "Remover um cliente", Order = 5)]
        public void DeletarCliente(Guid id)
        {
            _clienteService.Deletar(id);
        }
    }
}
