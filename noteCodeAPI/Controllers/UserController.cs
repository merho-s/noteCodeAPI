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
        private IAuthentication _authenticationService;
        private UserAppService _userService;

        public UserController(IAuthentication authenticationService, UserAppService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] string username, [FromForm] string password)
        {
            try
            {
                return Ok(await _authenticationService.LoginAsync(username, password));
            }
            catch (AuthenticationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPost("signup")]
        //public async Task<IActionResult> SignUpAsync([FromBody] UserRequestDTO userRequest)
        //{
        //    try
        //    {
        //        return Ok(await _userService.SignUpAsync(userRequest)); 
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpPost("signout")]
        public async Task<IActionResult> SignOutAsync()
        {
            try
            {
                return Ok(await _userService.SignOutAsync());
            }
            catch (Exception ex)
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

        [HttpPost("requestaccess")]
        public async Task<IActionResult> RequestAccessAsync(UserRequestDTO userRequest)
        {
            try
            {
                return Ok(await _userService.RequestAccessAsync(userRequest));
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        } 
    }
}
