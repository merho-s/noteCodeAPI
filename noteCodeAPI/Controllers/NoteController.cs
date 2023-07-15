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
            catch (NotFoundUserException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { ex.Message });
            }
            catch (TagsDontExistException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { ex.Message });
            }
            catch (UploadException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { ex.Message });
            }
            catch (DatabaseException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetNotesAsync()
        {
            try
            {
                List<NoteResponseDTO> notesResponse = await _noteService.GetNotesListAsync();
                return Ok(notesResponse);
            }
            catch (NotFoundUserException ex)
            { 
                return StatusCode(500, new { ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteByIdAsync(int id)
        {
            try
            {
                return Ok(await _noteService.GetSingleNoteAsync(id));
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, new { ex.Message });
            }
            catch (NotFoundUserException ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        //[Authorize("admin")]
        [HttpGet("testget")]
        public async Task<IActionResult> GetAllNotesAsync()
        {
            return Ok(await _noteService.GetAllNotesTestAsync());  
        }
    }
}
