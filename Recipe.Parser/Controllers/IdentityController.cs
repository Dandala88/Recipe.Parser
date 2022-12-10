using Microsoft.AspNetCore.Mvc;
using Core.JwtBuilder;
using System.Security.Claims;

namespace Recipe.Parser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _config;

        public IdentityController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string username, [FromQuery] string password)
        {
            var againstUsername = _config.GetSection("Credentials:Username").Value;
            var againstPassword = _config.GetSection("Credentials:Password").Value;

            if (againstUsername != username || againstPassword != password)
                return Forbid();

            var token = JwtTokenGenerator.GenerateToken(_config, new List<Claim>());
            return Ok(token);
        }
    }
}
