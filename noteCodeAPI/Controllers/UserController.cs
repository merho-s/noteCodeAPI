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
        private MailService _mailService;

        public UserController(IAuthentication authenticationService, UserAppService userService, MailService mailService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _mailService = mailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequestDTO loginRequest)
        {
            try
            {
                return Ok(await _authenticationService.LoginAsync(loginRequest.Username, loginRequest.Password));
            }
            catch (AuthenticationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
            catch (NotFoundException ex)
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
                var userResponse = await _userService.RequestAccessAsync(userRequest);
                _mailService.SendMail(userResponse.Email, userResponse.Username);
                return Ok(userResponse);

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("userstatus")]
        public async Task<IActionResult> GetUserStatusAsync(int userId)
        {
            try
            {
                return Ok(await _userService.GetUserStatusAsync(userId));
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
