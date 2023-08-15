using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noteCodeAPI.Services;

namespace noteCodeAPI.Controllers
{
    [Authorize("admin")]
    [Route("api/v1/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private UserAppService _userService;
        private NoteService _noteService;

        public AdminController(UserAppService userService, NoteService noteService)
        {
            _userService = userService;
            _noteService = noteService;
        }

        [HttpPost("whitelist/{id}")]
        public async Task<IActionResult> WhitelistUserAsync(int id)
        {
            try
            {
                return Ok(await _userService.WhitelistUserAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("waitingusers")]
        public async Task<IActionResult> GetAllWaitingUsersAsync()
        {
            try
            {
                return Ok(await _userService.GetAllWaitingUsersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GettAllUsersAsync()
        {
            try
            {
                return Ok(await _userService.GetAllUsersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("notes")]
        public async Task<IActionResult> GetAllNotesAsync()
        {
            try
            {
                return Ok(await _noteService.GetAllNotesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                return Ok(await _userService.DeleteUserAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
