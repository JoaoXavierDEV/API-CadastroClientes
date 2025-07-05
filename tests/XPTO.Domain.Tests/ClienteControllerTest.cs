using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using XPTO.Application.DTO;
using XPTO.Application.Interfaces;
using XPTO.Application.Mappings;
using XPTO.Application.Services;
using XPTO.Domain.Entities;
using XPTO.Domain.Interfaces;
using XPTO.Domain.Validation;
using XPTO.Infrastructure.Data;
using XPTO.Infrastructure.Data.Repositories;
using XPTO.Presentation.API.Controllers.v1;
using Xunit.Abstractions;

namespace XPTO.Domain.Tests
{
    public class ClienteControllerTest
    {
        private readonly Mapper _mapper;
        private readonly ILogger _logger;
        private readonly IClienteService _clienteService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<Cliente> _validatorCliente = new ClienteValidator();
        private readonly IValidator<Endereco> _validatorEndereco = new EnderecoValidator();

        private readonly ClienteController _controller;

        private readonly ITestOutputHelper _outputHelper;


        public ClienteControllerTest(ITestOutputHelper testOutputHelper)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToDTOMappingProfile>();
            });
            _mapper = new Mapper(configuration);

            ILogger<ClienteController> logger = LoggerFactory
                .Create(builder =>
                {
                    // builder.ClearProviders();
                    builder.AddConsole();
                    builder.AddDebug();
                    builder.SetMinimumLevel(LogLevel.Debug);
                })
                .CreateLogger<ClienteController>();

            _logger = logger;

            var repositoryCliente = new ClienteRepository(new ApplicationDbContextFactory().CreateDbContext());
            var repositoryEndereco = new EnderecoRepository(new ApplicationDbContextFactory().CreateDbContext());
            _clienteService = new ClienteService(repositoryCliente, _mapper, _validatorCliente, repositoryEndereco, _validatorEndereco);
            _clienteRepository = repositoryCliente;

            _controller = new ClienteController(logger, _clienteService, _clienteRepository, _validatorCliente);

            _outputHelper = testOutputHelper;

        }




        [Fact(DisplayName = "Retonar uma lista de clientes")]
        public void GetClientes_DeveRetornarListaDeClientes()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ClienteController>>();
            var mockService = new Mock<IClienteService>();
            var mockRepository = new Mock<IClienteRepository>();
            var mockValidator = new Mock<IValidator<Cliente>>();

            var clientesEsperados = new List<ClienteDTO>
            {
                new ClienteDTO { Id = Guid.NewGuid(), Nome = "João", Email = "joao@teste.com", Telefone = "123456" },
                new ClienteDTO { Id = Guid.NewGuid(), Nome = "Maria", Email = "maria@teste.com", Telefone = "654321" }
            };

            mockService.Setup(s => s.ObterTodosClientes()).Returns(clientesEsperados);

            var controller = new ClienteController(
                mockLogger.Object,
                mockService.Object,
                mockRepository.Object,
                mockValidator.Object
            );

            //await Task.Delay(2000); // Simula um atraso de 2 segundo
            // Act
            var resultado = controller.GetClientes();

            var result = mockService.Object.ObterTodosClientes();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.Equal("João", resultado[0].Nome);
            Assert.Equal("Maria", resultado[1].Nome);
        }

        [Fact(DisplayName = "Playground", Skip = "Falta refatorar")]
        public void CriarCliente_DeveRetornarCreated_QuandoClienteValido()
        {
            // Arrange
            //var mockLogger = new Mock<ILogger<ClienteController>>();


            //var repositoryCliente = new ClienteRepository
            //    (new ApplicationDbContextFactory().CreateDbContext());

            //var repositoryEndereco = new EnderecoRepository
            //    (new ApplicationDbContextFactory().CreateDbContext());

            //var mockService = new ClienteService(repositoryCliente, _mapper, _validatorCliente, repositoryEndereco, _validatorEndereco);
            _outputHelper.WriteLine("Iniciando teste CriarCliente_DeveRetornarCreated_QuandoClienteValido");

            var clienteDto = new ClienteDTO
            {
                Id = Guid.NewGuid(),
                Nome = "a",
                Email = "",
                Telefone = "999999999",
                Endereco = new EnderecoDTO()
            };


            //var entity = _mapper.Map<Cliente>(clienteDto);

            //var validate = _validatorCliente.Validate(entity);



            //var controller = new ClienteController(
            //    mockLogger.Object,
            //    mockService,
            //    repositoryCliente,
            //    _validatorCliente // Supondo que você tenha um validador de cliente implementado
            //);



            // Act
            var resultado = _controller.CriarCliente(clienteDto);

            // Assert
            var cliente = Assert.IsType<BadRequestObjectResult>(resultado);

            var clienteDetails = Assert.IsType<ValidationProblemDetails>(cliente.Value);



            Assert.NotEmpty(clienteDetails.Errors);

            Assert.Contains("Email", clienteDetails.Errors.Keys);
            Assert.Contains("O email deve ser um endereço de email válido.", clienteDetails.Errors["Email"]);


            Assert.Equal(3, clienteDetails.Errors["Email"].Length);

            // Assert.Collection = Todos os itens abaixo devem passar
            //Assert.Collection(cliente.ValidationResult.Errors,
            //    x => Assert.Equal("O email é obrigatório.", x.ErrorMessage),
            //    x => Assert.Equal("O email deve ser um endereço de email válido.", x.ErrorMessage),
            //    x => Assert.Equal("O email deve ter entre 1 e 50 caracteres.", x.ErrorMessage));

            Assert.Collection(clienteDetails.Errors["Email"],
                Value => Assert.Equal("O email é obrigatório.", Value),
                Value => Assert.Equal("O email deve ser um endereço de email válido.", Value),
                Value => Assert.Equal("O email deve ter entre 1 e 50 caracteres.", Value)
                );

        }

        [Fact(DisplayName = "Adicionar Cliente Válido")]
        [Trait("Cliente", "Post")]
        public void ObterClientePorID_DeveRetornarOk_QuandoClienteExistir()
        {
            // Arrange
            //var mockLogger = new Mock<ILogger<ClienteController>>();


            //var repositoryCliente = new ClienteRepository
            //    (new ApplicationDbContextFactory().CreateDbContext());

            //var repositoryEndereco = new EnderecoRepository
            //    (new ApplicationDbContextFactory().CreateDbContext());

            //var mockService = new ClienteService(repositoryCliente, _mapper, _validatorCliente, repositoryEndereco, _validatorEndereco);
            _outputHelper.WriteLine("Iniciando teste CriarCliente_DeveRetornarCreated_QuandoClienteValido");

            var clienteDto = new ClienteDTO("José Xavier", "jose@gmail.com", "2178985231",
                    new EnderecoDTO("Rua Prefeito Jose", "1024", "Nova Iguaçu", "RJ", "20258-987") { Id = Guid.Parse("962AE9D1-A200-4FBF-81AA-72837C092B67") })
            {
                Id = Guid.Parse("ac0ff7b5-85fc-43fc-8e39-a7d5f5910582")
            };


            // Act
            var resultado = _controller.CriarCliente(clienteDto);

            // Assert
            var cliente = Assert.IsType<CreatedResult>(resultado);

            var clienteDetails = Assert.IsType<ClienteDTO>(cliente.Value);


            Assert.Equal(clienteDto, cliente.Value);


        }

        [Fact(DisplayName = "Deletar Cliente por ID", Skip = "Falta Implementar")]
        [Trait("Cliente", "Delete")]
        public void DeletarCliente_DeveRetornarOK()
        {

        }
    }
}