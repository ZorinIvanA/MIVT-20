using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebService.Domain.Entities.Service;
using WebService.Service.Domain.Interfaces;

namespace WebService.Service.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;

        private readonly string _forecastServiceUri;
        private readonly IForecastRepository _forecastRepository;

        public WeatherForecastController(IForecastRepository repository,
            ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _forecastServiceUri = configuration.GetSection("ForecastService").Value;
            _forecastRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public WeatherForecast[] Get()
        {
            return _forecastRepository.GetForecast();
        }

        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                return Created("https://localhost:49153/weatherforecast", _forecastServiceUri);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "It's a mistake");
            }
        }
    }
}
