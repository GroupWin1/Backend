using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = index,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                Price = Random.Shared.Next(200000, 5000000),
            })
            .ToArray();
            return StatusOK(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Detail(int id)
        {
            var model =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = index,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                Price = Random.Shared.Next(200000, 5000000),
            }).FirstOrDefault(f => f.Id.Equals(id));
            if (model == null)
                return Problem("Data not found", statusCode: StatusCodes.Status400BadRequest);
            return StatusOK(model);
        }

        private IActionResult StatusOK(object? value) => StatusCode(StatusCodes.Status200OK, value);
    }
}
