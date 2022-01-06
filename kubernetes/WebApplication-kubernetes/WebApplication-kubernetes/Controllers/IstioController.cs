using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApplication_kubernetes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IstioController : ControllerBase
    {
        private static Random _rnd = new();
        const string Version = "A";

        [HttpGet("version")]
        public IActionResult GetVersion()
        {

            return Ok($"App is running on version: {Version}");
        }

        [HttpGet("circuit-breaker")]
        public IActionResult CircuitBreaker()
        {
            bool.TryParse(Environment.GetEnvironmentVariable("SIMULATE_ERROR"), out bool error);

            var randomSeconds = _rnd.Next(1, 6);

            if (error)
            {
                Task.Delay(TimeSpan.FromSeconds(randomSeconds))
                    .GetAwaiter()
                    .GetResult();

                return new StatusCodeResult(504);
            }

            return Ok($"App is running");
        }
    }
}
