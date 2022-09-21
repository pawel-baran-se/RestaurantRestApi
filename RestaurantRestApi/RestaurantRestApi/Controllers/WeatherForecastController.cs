using Microsoft.AspNetCore.Mvc;

namespace RestaurantRestApi.Controllers
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

        [HttpGet("{maxResults}")]
        public ActionResult<IEnumerable<WeatherForecast>> Get([FromRoute] int maxResults, [FromQuery] int minTemp, [FromQuery] int maxTemp)
        {
            if (maxResults < 0)
                return BadRequest();

            if (maxTemp >= minTemp)
                return BadRequest();

            return Enumerable.Range(1, maxResults).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-minTemp, maxTemp),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("currentDay/{max}")]
        public IEnumerable<WeatherForecast> Get2([FromQuery]int take, [FromRoute]int max )
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
        [Route("hello")]
        public ActionResult<string> HelloApi([FromQuery]string name)
        {
            HttpContext.Response.StatusCode = 401;
            return $"Hello from Api {name}";
        }


    }
}