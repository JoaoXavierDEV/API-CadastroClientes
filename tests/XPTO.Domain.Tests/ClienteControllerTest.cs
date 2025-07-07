using System.Data.Entity;
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
using XPTO.Domain.Exceptions;
using XPTO.Domain.Interfaces;
using XPTO.Domain.Validation;
using XPTO.Infrastructure.Data;
using XPTO.Infrastructure.Data.Repositories;
using XPTO.Presentation.API.Controllers.v1;
using Xunit.Abstractions;

namespace XPTO.Domain.Tests
{
    [Collection("XPTO Cliente Controller v1")]
    [Trait("Cliente", "Cliente Controller v1")]
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




        #region GET

        [Fact(DisplayName = "Obter Cliente por ID")]
        [Trait("Cliente", "Get")]
        public void ObterClientePorID_DeveRetornarCliente()
        {
            // Arrange
            var cliente = _controller.ObterClientePorID(Guid.Parse("ac0ff7b5-85fc-43fc-8e39-a7d5f5910582"));

            var clienteResult = Assert.IsType<ActionResult<ClienteDTO>>(cliente);

            var dto = Assert.IsType<OkObjectResult>(clienteResult.Result);

            var clienteDetails = Assert.IsType<ClienteDTO>(dto.Value);

            Assert.NotNull(cliente);

            Assert.NotNull(clienteDetails.Nome);
        }

        [Fact(DisplayName = "Obtêm uma lista de clientes")]
        [Trait("Cliente", "Get")]
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

            var resultado2 = _controller.GetClientes();

            var result = mockService.Object.ObterTodosClientes();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.Equal("João", resultado[0].Nome);
            Assert.Equal("Maria", resultado[1].Nome);

            Assert.NotEmpty(resultado2);
        }

        #endregion

        #region Post

        [Fact(DisplayName = "Adicionar Cliente com Endereço")]
        [Trait("Cliente", "Post")]
        public void AdicionarClienteComEndereco_DeveRetornarOk()
        {
            // Arrange
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



            var clienteRepo = _clienteRepository.Consultar().FirstOrDefault(x => x.Id == clienteDto.Id);

            var clienteDtoRepo = _mapper.Map<ClienteDTO>(clienteRepo);

            Assert.Equal(clienteDto, clienteDtoRepo);
        }

        [Fact(DisplayName = "Adicionar Cliente com email já Cadastrado")]
        [Trait("Cliente", "Post")]
        public void ObterClientePorID_DeveRetornarError_QuandoClienteExistir()
        {
            // Arrange
            //var clienteDto = new ClienteDTO("José Xavier", "jose@gmail.com", "2178985231",
            //        new EnderecoDTO("Rua Prefeito Jose", "1024", "Nova Iguaçu", "RJ", "20258-987") { Id = Guid.Parse("962AE9D1-A200-4FBF-81AA-72837C092B67") })
            //{
            //    Id = Guid.Parse("ac0ff7b5-85fc-43fc-8e39-a7d5f5910582")
            //};

            var endereco = new EnderecoDTO("Rua D", "1024", "Cidade", "RJ", "20258-987") { Id = Guid.Parse("962AE9D1-A200-4FBF-81AA-72837C092B67") };

            var clienteDto = new ClienteDTO("João", "joao@outlook.com", "21 97898 5231", endereco)
            {
                Id = Guid.Parse("ac0ff7b5-85fc-43fc-8e39-a7d5f5910582")
            };

            // Act & Assert
            var resultado = _controller.CriarCliente(clienteDto);

            var resultadoDuplicado = _controller.CriarCliente(clienteDto);

            // chamada direto na service lança exception do tipo DomainExceptionValidation
            Assert.Throws<DomainExceptionValidation>(() => _clienteService.Adicionar(clienteDto));

            //  esperado que o resultado seja BadRequestObjectResult
            var cliente = Assert.IsType<BadRequestObjectResult>(resultadoDuplicado);

            // esperado que o valor seja ValidationProblemDetails
            var clienteDetails = Assert.IsType<ValidationProblemDetails>(cliente.Value);

            Assert.NotEmpty(clienteDetails.Errors);

            Assert.Contains("Email", clienteDetails.Errors.Keys);

            Assert.Collection(clienteDetails.Errors["Email"],
                Value => Assert.Equal("Já existe um cliente com este email.", Value));

            Assert.Single(clienteDetails.Errors["Email"], "Já existe um cliente com este email.");
        }

        [Fact(DisplayName = "Adicionar Cliente sem endereço")]
        [Trait("Cliente", "Post")]
        public void AdicionarClienteSemEndereco_DeveRetornar()
        {
            // Arrange

            var clienteDto = new ClienteDTO("José Xavier", "jose@gmail.com", "2178985231")
            {
                Id = Guid.Parse("ac0ff7b5-85fc-43fc-8e39-a7d5f5910582")
            };


            // Act
            var resultado = _controller.CriarCliente(clienteDto);

            // Assert
            var cliente = Assert.IsType<CreatedResult>(resultado);

            var clienteDetails = Assert.IsType<ClienteDTO>(cliente.Value);


            Assert.Equal(clienteDto, cliente.Value);



            var clienteRepo = _clienteRepository.Consultar().FirstOrDefault(x => x.Id == clienteDto.Id);

            var clienteDtoRepo = _mapper.Map<ClienteDTO>(clienteRepo);

            Assert.Equal(clienteDto, clienteDtoRepo);
        }

        [Fact(DisplayName = "Adicionar Cliente Inválido")]
        [Trait("Cliente", "Post")]
        public void AdicionarClienteInvalido_DeveRetornarBadRequest()
        {
            // Arrange
            var clienteDto = new ClienteDTO
            {
                Id = Guid.NewGuid(),
                Nome = "a",
                Email = "",
                Telefone = "999999999",
                Endereco = new EnderecoDTO()
            };

            // Act
            var resultado = _controller.CriarCliente(clienteDto);

            // Assert
            var cliente = Assert.IsType<BadRequestObjectResult>(resultado);
            var clienteDetails = Assert.IsType<ValidationProblemDetails>(cliente.Value);
            Assert.NotEmpty(clienteDetails.Errors);
            Assert.Contains("Email", clienteDetails.Errors.Keys);
            Assert.Contains("O email deve ser um endereço de email válido.", clienteDetails.Errors["Email"]);
            Assert.Equal(3, clienteDetails.Errors["Email"].Length);
            Assert.Collection(clienteDetails.Errors["Email"],
                Value => Assert.Equal("O email é obrigatório.", Value),
                Value => Assert.Equal("O email deve ser um endereço de email válido.", Value),
                Value => Assert.Equal("O email deve ter entre 1 e 50 caracteres.", Value)
                );
        }

        #endregion

        #region Delete

        [Fact(DisplayName = "Deletar Cliente com endereço")]
        [Trait("Cliente", "Delete")]
        public void DeletarClienteComEndereco_DeveRetornarOK()
        {
            var clienteList = _clienteRepository.Consultar().ToList();

            // Arrange
            var clienteId = Guid.Parse("ac0ff7b5-85fc-43fc-8e39-a7d5f5910582");

            // Act
            var resultado = _controller.DeletarCliente(clienteId);

            // Assert
            var cliente = Assert.IsType<OkObjectResult>(resultado);

            Assert.NotNull(cliente);

            var clienteRepo = _clienteRepository.ObterPorId(clienteId);

            Assert.Null(clienteRepo); // Verifica se o cliente foi realmente removido

            _outputHelper.WriteLine($"Cliente removido com sucesso.");
        }

        [Fact(DisplayName = "Deletar Cliente sem endereço")]
        [Trait("Cliente", "Delete")]
        public void DeletarCliente_DeveRetornarOK()
        {
            var clienteList = _clienteRepository.Consultar().ToList();

            // Arrange
            var clienteId = Guid.Parse("323799cb-54e0-4804-8076-b2ab1f4a152d");

            // Act
            var resultado = _controller.DeletarCliente(clienteId);

            // Assert
            var cliente = Assert.IsType<OkObjectResult>(resultado);

            Assert.NotNull(cliente);

            var clienteRepo = _clienteRepository.ObterPorId(clienteId);

            Assert.Null(clienteRepo); // Verifica se o cliente foi realmente removido

            _outputHelper.WriteLine($"Cliente removido com sucesso.");
        }

        [Fact(DisplayName = "Deletar Cliente Inválido")]
        [Trait("Cliente", "Delete")]
        public void DeletarCliente_DeveRetornarErro()
        {
            // Arrange
            var clienteId = Guid.NewGuid(); // ID de um cliente que não existe
            // Act
            var resultado = _controller.DeletarCliente(clienteId);
            // Assert
            var cliente = Assert.IsType<NotFoundObjectResult>(resultado);

            Assert.Equal("Erro ao remover cliente: Cliente não encontrado.", cliente.Value);
        }

        #endregion

        #region PUT

        [Fact(DisplayName = "Atualizar Cliente")]
        [Trait("Cliente", "Put")]
        public void AtualizarCliente_DeveRetornarOK()
        {
            var endereco = new EnderecoDTO("Avenida ", "999", "Volta Redonda", "RJ", "20300-000") { Id = Guid.Parse("962AE9D1-A200-4FBF-81AA-72837C092B67") };
            var cliente = new ClienteDTO("João Fernando Xavier", "joao@outlook.com", "21 97898 0000", endereco)
            {
                Id = Guid.Parse("ac0ff7b5-85fc-43fc-8e39-a7d5f5910582")
            };

            // O EF só armazena o valor original se o objeto está sendo rastreado (sem AsNoTracking()),
            // mas mesmo assim, após o SaveChanges, o valor original é atualizado para o novo valor. 

            // Cópia por Construtor
            Cliente clienteAtualCopia = new Cliente(_clienteRepository.Consultar()
                    .AsNoTracking() // não rastreia as alterações, após SaveChanges, o valor original é atualizado 
                                    // não mantém o estado original
                    .First(x => x.Id == cliente.Id));

            // Cópia por Reflection
            Cliente clienteAtualCopia2 = _clienteRepository.Consultar()
                    .AsNoTracking()
                    .First(x => x.Id == cliente.Id).Copiar<Cliente>();


            // arrange

            var result = _controller.AtualizarCliente(cliente.Id, cliente);

            // act
            var clienteResult = Assert.IsType<ActionResult<ClienteDTO>>(result);

            var dto = Assert.IsType<OkObjectResult>(clienteResult.Result);

            var clienteDetails = Assert.IsType<ClienteDTO>(dto.Value);

            var clienteAtualizado = _clienteRepository.Consultar()
                .AsNoTracking()
                .First(x => x.Id == cliente.Id);

            Assert.NotEqual(clienteAtualCopia, clienteAtualizado);

            Assert.NotEqual(clienteAtualCopia2, clienteAtualizado);

            Assert.NotEqual(clienteAtualCopia.Endereco, clienteAtualizado.Endereco);
            Assert.NotEqual(clienteAtualCopia2.Endereco, clienteAtualizado.Endereco);
        }

        [Fact(DisplayName = "Atualizar Cliente remover endereço")]
        [Trait("Cliente", "Put")]
        public void AtualizarClienteParaRemoverEndereco_DeveRetornarOK()
        {
            var cliente = new ClienteDTO("João Fernando Xavier", "joao@outlook.com", "21 97898 0000")
            {
                Id = Guid.Parse("ac0ff7b5-85fc-43fc-8e39-a7d5f5910582")
            };

            // O EF só armazena o valor original se o objeto está sendo rastreado (sem AsNoTracking()),
            // mas mesmo assim, após o SaveChanges, o valor original é atualizado para o novo valor. 

            Cliente clienteAtualCopia = new Cliente(_clienteRepository.Consultar()
                    .AsNoTracking() // não rastreia as alterações, após SaveChanges, o valor original é atualizado 
                                    // não mantém o estado original
                    .First(x => x.Id == cliente.Id));

            Cliente clienteAtualCopia2 = _clienteRepository.Consultar()
                    .AsNoTracking()
                    .First(x => x.Id == cliente.Id).Copiar<Cliente>();


            // arrange

            var result = _controller.AtualizarCliente(cliente.Id, cliente);

            // act
            var clienteResult = Assert.IsType<ActionResult<ClienteDTO>>(result);

            var dto = Assert.IsType<OkObjectResult>(clienteResult.Result);

            var clienteDetails = Assert.IsType<ClienteDTO>(dto.Value);

            var clienteAtualizado = _clienteRepository.Consultar()
                .AsNoTracking()
                .First(x => x.Id == cliente.Id);

            Assert.NotEqual(clienteAtualCopia, clienteAtualizado);

            Assert.NotEqual(clienteAtualCopia2, clienteAtualizado);

        }

        #endregion

        //[Fact(DisplayName = "Playground", Skip = "Falta refatorar")]
        //public void CriarCliente_DeveRetornarCreated_QuandoClienteValido()
        //{
        //    // Arrange
        //    //var mockLogger = new Mock<ILogger<ClienteController>>();


        //    //var repositoryCliente = new ClienteRepository
        //    //    (new ApplicationDbContextFactory().CreateDbContext());

        //    //var repositoryEndereco = new EnderecoRepository
        //    //    (new ApplicationDbContextFactory().CreateDbContext());

        //    //var mockService = new ClienteService(repositoryCliente, _mapper, _validatorCliente, repositoryEndereco, _validatorEndereco);
        //    _outputHelper.WriteLine("Iniciando teste CriarCliente_DeveRetornarCreated_QuandoClienteValido");

        //    var clienteDto = new ClienteDTO
        //    {
        //        Id = Guid.NewGuid(),
        //        Nome = "a",
        //        Email = "",
        //        Telefone = "999999999",
        //        Endereco = new EnderecoDTO()
        //    };


        //    //var entity = _mapper.Map<Cliente>(clienteDto);

        //    //var validate = _validatorCliente.Validate(entity);



        //    //var controller = new ClienteController(
        //    //    mockLogger.Object,
        //    //    mockService,
        //    //    repositoryCliente,
        //    //    _validatorCliente // Supondo que você tenha um validador de cliente implementado
        //    //);



        //    // Act
        //    var resultado = _controller.CriarCliente(clienteDto);

        //    // Assert
        //    var cliente = Assert.IsType<BadRequestObjectResult>(resultado);

        //    var clienteDetails = Assert.IsType<ValidationProblemDetails>(cliente.Value);



        //    Assert.NotEmpty(clienteDetails.Errors);

        //    Assert.Contains("Email", clienteDetails.Errors.Keys);
        //    Assert.Contains("O email deve ser um endereço de email válido.", clienteDetails.Errors["Email"]);


        //    Assert.Equal(3, clienteDetails.Errors["Email"].Length);

        //    // Assert.Collection = Todos os itens abaixo devem passar
        //    //Assert.Collection(cliente.ValidationResult.Errors,
        //    //    x => Assert.Equal("O email é obrigatório.", x.ErrorMessage),
        //    //    x => Assert.Equal("O email deve ser um endereço de email válido.", x.ErrorMessage),
        //    //    x => Assert.Equal("O email deve ter entre 1 e 50 caracteres.", x.ErrorMessage));

        //    Assert.Collection(clienteDetails.Errors["Email"],
        //        Value => Assert.Equal("O email é obrigatório.", Value),
        //        Value => Assert.Equal("O email deve ser um endereço de email válido.", Value),
        //        Value => Assert.Equal("O email deve ter entre 1 e 50 caracteres.", Value)
        //        );

        //}
    }
}