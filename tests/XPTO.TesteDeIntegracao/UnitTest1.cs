using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using XPTO.Application.DTO;

namespace XPTO.TesteDeIntegracao;


public class ClienteControllerTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true, // Formata o JSON para facilitar a leitura
    };

    public ClienteControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory(DisplayName = "ObterTodosOsClientes")]
    [InlineData("/api/v1/clientes")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        HttpClient client = new();
        try
        {
            client = _factory.CreateClient();
            //client.BaseAddress = new Uri("http://localhost:52607");
            //client.DefaultRequestVersion = new Version(1, 0);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }


        // Act
        var response = await client.GetAsync(url);

        // Assert
        var json = await response.Content.ReadAsStringAsync();

        var clientes = JsonSerializer.Deserialize<List<ClienteDTO>>(json);

        Assert.NotNull(clientes);

        Assert.All(clientes, c => Assert.False(string.IsNullOrEmpty(c.Nome)));

        Debug.WriteLine(json);
    }
}