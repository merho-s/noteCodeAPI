using Microsoft.AspNetCore.Mvc;
using noteCodeAPI.Services;
using noteCodeAPI.Services.Interfaces;

namespace noteCodeAPI.Controllers
{
    [ApiController]
    [Route("api/v1/login")]
    public class UserController : ControllerBase
    {
        private UserAppService _userService;

        public UserController(UserAppService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            try
            {
                return Ok(_userService.Login(username, password));
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
