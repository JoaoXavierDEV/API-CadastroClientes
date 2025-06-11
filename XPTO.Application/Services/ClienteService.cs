using AutoMapper;
using FluentValidation;
using XPTO.Application.DTOs;
using XPTO.Application.Interfaces;
using XPTO.Domain.Entities;
using XPTO.Domain.Exceptions;
using XPTO.Domain.Interfaces;

namespace XPTO.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Cliente> _validator;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper, IValidator<Cliente> validator, IEnderecoRepository enderecoRepository)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _validator = validator;
            _enderecoRepository = enderecoRepository;
        }

        public void Adicionar(ClienteDTO clienteDto)
        {
            ArgumentNullException.ThrowIfNull(clienteDto, nameof(clienteDto));

            var emailExists = _clienteRepository.Consultar().Any(x => x.Email == clienteDto.Email);

            if (emailExists)
                throw new DomainExceptionValidation("Já existe um cliente com este email.", nameof(clienteDto.Email));

            var cliente = _mapper.Map<Cliente>(clienteDto);

            var entityValid = _validator.Validate(cliente);

            if (!entityValid.IsValid)
                throw new DomainExceptionValidation(entityValid.ToDictionary());

            _clienteRepository.Adicionar(cliente);
        }

        public ClienteDTO ObterPorId(Guid id)
        {
            var cliente = _clienteRepository.ObterPorId(id);

            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            var clienteDto = _mapper.Map<ClienteDTO>(cliente);

            return clienteDto;
        }

        public IEnumerable<ClienteDTO> ObterTodosClientes()
        {
            var clientes = _clienteRepository.ObterTodosClientes();
            return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
        }

        public void Deletar(Guid id)
        {
            var cliente = _clienteRepository.ObterPorId(id);

            if (cliente is null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            var endereco = _enderecoRepository.ObterPorId(cliente.Endereco.Id);

            _enderecoRepository.Remover(endereco.Id);

            _clienteRepository.Remover(id);
        }

        public void Atualizar(ClienteDTO dto)
        {
            try
            {
                var cliente = _clienteRepository.ObterPorId(dto.Id);

                cliente.Nome = dto.Nome;
                cliente.Email = dto.Email;
                cliente.Telefone = dto.Telefone;

                cliente.Endereco.Cidade = dto.Endereco.Cidade;
                cliente.Endereco.Estado = dto.Endereco.Estado;
                cliente.Endereco.Rua = dto.Endereco.Rua;
                cliente.Endereco.Numero = dto.Endereco.Numero;
                cliente.Endereco.Cep = dto.Endereco.Cep;

                // cliente = _mapper.Map<Cliente>(dto);

                _clienteRepository.Atualizar(cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
