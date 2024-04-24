using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Utilities;

namespace DatingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Dating _dating;

        public LoginController(Dating dating)
        {
            _dating = dating;
        }

        // Model representing login credentials
        public class Login
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        // POST api/login/authenticateuser
        [HttpPost("authenticateuser")]
        public IActionResult AuthenticateUser([FromBody] Login model)
        {

            // Check if model is null
            if (model == null)
            {
                return BadRequest("Invalid login credentials: Credentials object is null.");
            }

            // Validate username and password
            int count = _dating.validateLogin(model.Username, model.Password);

            if (count != 1)
            {
                return Unauthorized("Invalid username or password.");
            }




            HttpContext.Session.SetString("username", model.Username);

            return Ok(new { message = "User authenticated successfully" });

        }
    }
}
