using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noteCodeAPI.DTOs;
using noteCodeAPI.Exceptions;
using noteCodeAPI.Services;
using noteCodeAPI.Services.Interfaces;

namespace noteCodeAPI.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private IAuthentication _AuthenticationService;
        private UserAppService _userService;

        public UserController(IAuthentication AuthenticationService, UserAppService userService)
        {
            _AuthenticationService = AuthenticationService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] string username, [FromForm] string password)
        {
            try
            {
                return Ok(await _AuthenticationService.LoginAsync(username, password));
            }
            catch (AuthenticationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] UserRequestDTO userRequest)
        {
            try
            {
                return Ok(await _userService.SignUpAsync(userRequest)); 
            }
            catch (Exception ex)
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
            catch (NotFoundUserException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("editme")]
        public async Task<IActionResult> EditLoggedUserAsync(UserRequestDTO userRequest)
        {
            try
            {
                return Ok(await _userService.EditLoggedUserAsync(userRequest));
            }
            catch (NotFoundUserException ex)
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
