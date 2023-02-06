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
        public IActionResult PostNote([FromBody] NoteRequestDTO noteRequest)
        {
            try
            {
                NoteResponseDTO noteResponse = _noteService.AddNote(noteRequest);
                return Ok(noteResponse);
            } 
            catch (NotLoggedUserException ex)
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
        public IActionResult GetNotes()
        {
            try
            {
                List<NoteResponseDTO> notesResponse = _noteService.GetNotesList();
                return Ok(notesResponse);
            }
            catch (NotLoggedUserException ex)
            { 
                return StatusCode(500, new { ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetNoteById(int id)
        {
            try
            {
                return Ok(_noteService.GetSingleNote(id));
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, new { ex.Message });
            }
            catch (NotLoggedUserException ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        [HttpGet("testget")]
        public IActionResult GetAllNotes()
        {
            return Ok(_noteService.GetAllNotesTest());  
        }
    }
}
