using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noteCodeAPI.DTOs;
using noteCodeAPI.Exceptions;
using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using noteCodeAPI.Services;
using Swashbuckle.Swagger;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace noteCodeAPI.Controllers
{
    [Route("api/v1/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
    
        private NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostNoteAsync([FromBody] NoteRequestDTO noteRequest/*, [FromForm] IFormFile imageFile*/)
        {
            try
            {
                NoteResponseDTO noteResponse = await _noteService.AddNoteAsync(noteRequest);
                return Ok(noteResponse);
            } 
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
            catch (TagsDontExistException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500,ex.Message);
            }
            catch (UploadException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserNotesAsync()
        {
            try
            {
                List<NoteResponseDTO> notesResponse = await _noteService.GetUserNotesAsync();
                return Ok(notesResponse);
            }
            catch (NotFoundException ex)
            { 
                return StatusCode(500,ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserNoteByGuidAsync(Guid id)
        {
            try
            {
                return Ok(await _noteService.GetSingleUserNoteAsync(id));
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserNoteAsync(Guid id)
        {
            try
            {
                return Ok(await _noteService.DeleteUserNoteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditUserNoteAsync(NoteRequestDTO noteRequest)
        {
            try
            {
                return Ok(await _noteService.EditNoteAsync(noteRequest));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
