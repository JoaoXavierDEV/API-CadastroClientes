using Microsoft.AspNetCore.Mvc;
using XPTO.Domain.Entities;
using XPTO.Infrastructure.Data.Context;

namespace XPTO.Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationDbContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Cliente> GetClientes()
        {
            var clientes = _context.Cliente.ToList();

            var enderecos = _context.Endereco.ToList();

            return clientes;

        }
    }
}
