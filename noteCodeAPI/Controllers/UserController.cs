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
                await _userService.SignOutAsync();
                return Ok("User disconnected.");
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
                return Ok(await _userService.AddToWaitingUsersAsync(userRequest));
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        } 

        [Authorize("admin")]
        [HttpPost("whitelist/{id}")]
        public async Task<IActionResult> WhitelistUserAsync(int id)
        {
            try
            {
                return Ok(await _userService.WhitelistUserAsync(id));
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize("admin")]
        [HttpGet("waitingusers")]
        public async Task<IActionResult> GetAllWaitingUsersAsync()
        {
            try
            {
                return Ok(await _userService.GetAllWaitingUsersAsync());
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }



    }
}
