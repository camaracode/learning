using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_kubernetes.Controllers
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

        private static DateTime _startApp = DateTime.Now;
        private static Random _rnd = new();

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            var duration = DateTime.Now.Subtract(_startApp).TotalSeconds;

            if (duration < 10)
            {
                _logger.LogError("application is not ready");
                throw new Exception("application is not ready");
            }

            return Ok($"App is running at: {duration} seconds");
        }

        [HttpGet("hello")]
        public string Hello()
        {
            string name = Environment.GetEnvironmentVariable("MYNAME") ?? "N/A";

            return $"Welcome {name}, {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
        }

        [HttpGet("generate-guid")]
        public IEnumerable<string> GenerateGuids()
        {
            try
            {
                var delay = new[]
                {
                    true, false
                };

                var useDelay = delay[_rnd.Next(delay.Length)];
                if (useDelay)
                    Task.Delay(TimeSpan.FromSeconds(1)).GetAwaiter().GetResult();

                return Enumerable.Range(1, 100)
                        .Select(s => Guid.NewGuid().ToString())
                        .ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("skills")]
        public async Task<IActionResult> ReadFileAsync()
        {
            try
            {
                var skills = await System.IO.File.ReadAllTextAsync(@"staticfiles/skills.txt");
                return Ok(skills);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error reading file: {ex.Message}");
            }
        }

        [HttpGet("secret")]
        public async Task<IActionResult> Secret()
        {
            var username = Environment.GetEnvironmentVariable("USERNAME") ?? "USERNAME";
            var password = Environment.GetEnvironmentVariable("PASSWORD") ?? "PASSWORD";

            return Ok($"Username: {username}, Password: {password}");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 100).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
