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
        private readonly IValidator<Cliente> _clienteValidator;
        private readonly IValidator<Endereco> _enderecoValidator;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper, IValidator<Cliente> validator, IEnderecoRepository enderecoRepository, IValidator<Endereco> enderecoValidator)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _clienteValidator = validator;
            _enderecoRepository = enderecoRepository;
            _enderecoValidator = enderecoValidator;
        }

        public void Adicionar(ClienteDTO clienteDto)
        {
            ArgumentNullException.ThrowIfNull(clienteDto, nameof(clienteDto));

            var emailExists = _clienteRepository.Consultar().Any(x => x.Email == clienteDto.Email);

            if (emailExists)
                throw new DomainExceptionValidation("Já existe um cliente com este email.", nameof(clienteDto.Email));

            var cliente = _mapper.Map<Cliente>(clienteDto);

            var clienteValidade = _clienteValidator.Validate(cliente);

            if (!clienteValidade.IsValid)
                throw new DomainExceptionValidation(clienteValidade.ToDictionary());

            if (cliente.Endereco is not null)
            {
                var enderecoValidade = _enderecoValidator.Validate(cliente.Endereco);

                if (!enderecoValidade.IsValid)
                    throw new DomainExceptionValidation(enderecoValidade.ToDictionary());
            }

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
            //var cliente = _clienteRepository.ObterPorId(id);
            var cliente = _clienteRepository.Consultar<Cliente>().FirstOrDefault(x => x.Id == id);

            if (cliente is null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            if (cliente.Endereco is not null)
            {
                //var endereco = _enderecoRepository.Consultar(). .FirstOrDefault(x => x.Id == cliente.Endereco.Id);

                _enderecoRepository.Remover(cliente.Endereco.Id).Wait();
            }

            _clienteRepository.Remover(id);
        }

        public void Atualizar(ClienteDTO dto)
        {
            try
            {
                var cliente = _clienteRepository.ObterPorId(dto.Id);

                if (cliente is null)
                    throw new Exception("Cliente não encontrado");

                var clienteValidade = _clienteValidator.Validate(_mapper.Map<Cliente>(dto));

                DomainExceptionValidation.When(!clienteValidade.IsValid, clienteValidade.ToDictionary());

                cliente.Nome = dto.Nome;
                cliente.Email = dto.Email;
                cliente.Telefone = dto.Telefone;

                if (dto.Endereco is not null)
                {
                    var enderecoValidade = _enderecoValidator.Validate(_mapper.Map<Endereco>(dto.Endereco));

                    if (!enderecoValidade.IsValid)
                        throw new DomainExceptionValidation(enderecoValidade.ToDictionary());

                    var novoEndereco = new Endereco(rua: dto.Endereco.Rua, numero: dto.Endereco.Numero, cidade: dto.Endereco.Cidade, estado: dto.Endereco.Estado, cep: dto.Endereco.Cep);

                    cliente.SetEndereco(novoEndereco);
                }
                else
                {
                    cliente.RemoverEndereco();
                }

                _clienteRepository.Atualizar(cliente);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
