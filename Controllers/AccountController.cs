using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNETCore5Demo.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASPNETCore5Demo.Models;

namespace ASPNETCore5Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> logger;
        public JwtHelpers Jwt { get; set; }
        private readonly JwtHelpers jwt;
        public AccountController(ILogger<AccountController> logger, JwtHelpers jwt)
        {
            this.jwt = jwt;
            this.Jwt = jwt;
            this.logger = logger;
        }

        [HttpPost("~/login")]
        public ActionResult<LoginResul> Login(LoginModel model)
        {
            if (ValidateUser())
            {
                return new LoginResul{
                    Token = jwt.GenerateToken(model.UserName, 10)
                };
            }
            else
                return BadRequest();
        }

        private bool ValidateUser()
        {
            return true;
        }
    }
}