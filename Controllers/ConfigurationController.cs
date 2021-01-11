using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IOptions<JwtSettings> jwtSettings;
        private readonly ILogger logger;
        public ConfigurationController(ILogger<ConfigurationController> logger, IOptions<JwtSettings> jwtSettings)
        {
            this.logger = logger;
            this.jwtSettings = jwtSettings;
        }

        [HttpGet("")]
        public ActionResult<JwtSettings> GetJwtSettings()
        {
            logger.Log(LogLevel.Critical, "Critical Issuer {Issuer}", jwtSettings.Value.Issuer);
            logger.Log(LogLevel.Error, "Error Issuer {Issuer}", jwtSettings.Value.Issuer);
            logger.Log(LogLevel.Information, "information Issuer {Issuer}", jwtSettings.Value.Issuer);
            logger.Log(LogLevel.Debug, "Debug Issuer {Issuer}", jwtSettings.Value.Issuer);
            logger.Log(LogLevel.Trace, "Trace Issuer {Issuer}", jwtSettings.Value.Issuer);

            return jwtSettings.Value;
        }
    }
}