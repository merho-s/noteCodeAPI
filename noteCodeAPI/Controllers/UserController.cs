using Microsoft.AspNetCore.Mvc;
using noteCodeAPI.Exceptions;
using noteCodeAPI.Services;
using noteCodeAPI.Services.Interfaces;

namespace noteCodeAPI.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private ILogin _loginService;
        private UserAppService _userService;

        public UserController(ILogin loginService, UserAppService userService)
        {
            _loginService = loginService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] string username, [FromForm] string password)
        {
            try
            {
                return Ok(await _loginService.LoginAsync(username, password));
            }
            catch (AuthenticationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("bantoken")]
        public async Task<IActionResult> BanTokenAsync()
        {
            try
            {
                return Ok("This token is out of usage: " + await _userService.BanCurrentTokenAsync());
            }
            catch (NotLoggedUserException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}
