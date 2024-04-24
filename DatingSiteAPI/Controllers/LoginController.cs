using DatingSiteLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace DatingSiteAPI.Controllers
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

        // POST api/login/authenticateuser
        [HttpPost("authenticateuser")]
        public IActionResult AuthenticateUser([FromBody] dynamic credentials)
        {
            if (credentials == null || string.IsNullOrEmpty(credentials.Username) || string.IsNullOrEmpty(credentials.Password))
            {
                return BadRequest("Invalid login credentials");
            }

            string username = credentials.Username;
            string password = credentials.Password;

            // Assuming Dating class has validateLogin method
            int count = _dating.validateLogin(username, password);

            if (count == 1)
            {
                // Authentication successful
                // You can perform additional actions here if needed, such as setting up a session

                // Example of setting session data
                HttpContext.Session.SetString("username", username);

                return Ok("User authenticated successfully");
            }
            else
            {
                // Authentication failed
                return Unauthorized("Invalid username or password");
            }
        }
    }
}
