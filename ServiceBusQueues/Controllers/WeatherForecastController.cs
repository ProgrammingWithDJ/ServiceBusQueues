using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ServiceBusQueues.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly ServiceBusClient client;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ServiceBusClient client,
            ILogger<WeatherForecastController> logger)
        {
            this.client = client;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task Post(WeatherForecast data)
        {
            var connectionString = "";
            var client = new ServiceBusClient(connectionString);
            var sender = client.CreateSender("add-weather-data");
            var body = JsonSerializer.Serialize(data);
            var message = new ServiceBusMessage(body);
           await  sender.SendMessageAsync(message);
        }
    }
}