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

        //[Authorize]
        [HttpPost]
        public IActionResult PostNote([FromForm] NoteRequestDTO noteRequest)
        {
            try
            {
                NoteResponseDTO noteResponse = _noteService.AddNote(noteRequest);
                return Ok(noteResponse);
            } 
            catch (NotLoggedUserException ex)
            {
                return StatusCode(500, new { ex.Message });
            }
            catch (TagsDontExistException ex)
            {
                return StatusCode(500, new { ex.Message });
            }
            catch (UploadException ex)
            {
                return StatusCode(500, new { ex.Message });
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        //[Authorize]
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
    }
}
