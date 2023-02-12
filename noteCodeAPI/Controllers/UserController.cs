using Microsoft.AspNetCore.Mvc;
using noteCodeAPI.Services.Interfaces;

namespace noteCodeAPI.Controllers
{
    [ApiController]
    [Route("api/v1/login")]
    public class UserController : ControllerBase
    {
        private ILogin _login;

        public UserController(ILogin login)
        {
            _login = login;
        }

        [HttpPost]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            string token = _login.Login(username, password);
            if (token != null)
            {
                return Ok(token);
            }
            return StatusCode(402);
        }
    }
}
