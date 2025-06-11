using AutoMapper;
using XPTO.Application.DTOs;
using XPTO.Application.Interfaces;
using XPTO.Domain.Entities;
using XPTO.Domain.Interfaces;

namespace XPTO.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }
        public void Adicionar(ClienteDTO clienteDto)
        {
            ArgumentNullException.ThrowIfNull(clienteDto, nameof(clienteDto));

            //if (clienteDto.Id == Guid.Empty)
            //{
            //    throw new ArgumentException("O ID do cliente não pode ser vazio.", nameof(clienteDto.Id));
            //}

            var cliente = _mapper.Map<Cliente>(clienteDto);

            _clienteRepository.Adicionar(cliente);
        }

        public ClienteDTO ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClienteDTO> ObterTodosClientes()
        {
            throw new NotImplementedException();
        }

        public void Deletar(Guid id)
        {
            _clienteRepository.Remover(id);
        }

        public void Atualizar(ClienteDTO dto)
        {
            var cliente = _mapper.Map<Cliente>(dto);

            _clienteRepository.Atualizar(cliente);
        }
    }
}
