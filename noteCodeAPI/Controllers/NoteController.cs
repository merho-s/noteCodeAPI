using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noteCodeAPI.DTOs;
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
        public IActionResult PostNote([FromForm] NoteRequestDTO noteRequest, IFormFile imageFile)
        {
            try
            {
                NoteResponseDTO noteResponse = _noteService.AddNote(noteRequest, imageFile);
                return Ok(noteResponse);
            } catch (Exception ex)
            {
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
            catch (Exception ex)
            { 
                return StatusCode(500, new { ex.Message });
            }
        }
    }
}
