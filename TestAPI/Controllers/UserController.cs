using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Models;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IAuthService authService;
        //private readonly IConfiguration configuration;

        public UsersController(
            IAuthService authService
            //,IConfiguration configuration
            )
        {
            this.authService = authService;
            //this.configuration = configuration;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            var result = authService.Authenticate(user.Username, user.Password);

            if (!result)
                return Unauthorized(new { message = "Username or password is incorrect" });

            return Ok(user.Username);
        }


    }
}
