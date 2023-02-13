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
        private UserAppService _userService;

        public UserController(UserAppService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string username, [FromForm] string password)
        {
            try
            {
                return Ok(_userService.Login(username, password));
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

        [HttpPost("bantoken")]
        public IActionResult BanToken()
        {
            try
            {
                return Ok(_userService.AddUnusedActiveToken());
            }catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("unbantoken/{tokenId}")]
        public IActionResult UnbanToken(int tokenId)
        {
            try
            {
                return Ok(_userService.RemoveUnusedActiveToken(tokenId));
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                _userService.Logout();
                return Ok("User logged out.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
