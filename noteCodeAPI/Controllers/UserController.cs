using Microsoft.AspNetCore.Mvc;
using noteCodeAPI.Services.Interfaces;

namespace noteCodeAPI.Controllers
{
    public class UserController : ControllerBase
    {
        private ILogin _login;

        public UserController(ILogin login)
        {
            _login = login;
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string username, string password)
        {
            string token = _login.Login(username, password);
            if(token != null) 
            {
                return Ok(token);
            }
            return StatusCode(402);
        }
    }
}
