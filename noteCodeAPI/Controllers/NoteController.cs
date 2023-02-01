using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noteCodeAPI.DTOs;
using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using Swashbuckle.Swagger;
using System.Reflection;
using System.Security.Claims;

namespace noteCodeAPI.Controllers
{
    [Route("api/v1/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private NoteRepository _noteRepos;
        private UserAppRepository _userRepos;
        private CodetagRepository _codetagRepos;

        public NoteController(NoteRepository noteRepos, UserAppRepository userRepos, CodetagRepository tagRepos)
        {
            _noteRepos = noteRepos;
            _userRepos = userRepos;
            _codetagRepos = tagRepos;
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostNote([FromForm] NoteRequestDTO noteRequest, IFormFile image)
        {
            List<Codetag> codetags = new List<Codetag>();
            noteRequest.Codetags.ForEach(t => codetags.Add(_codetagRepos.GetByName(t.Name)));
            Note note = new Note()
            {
                Title = noteRequest.Title,
                Description = noteRequest.Description,
                Code = noteRequest.Code,
                Codetags = codetags
            };

            if (image == null || image.Length== 0 )
            {
                return BadRequest("No image file found.");
            }

            var userClaims = HttpContext.User;
            string username = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            UserApp loggedUser = _userRepos.SearchOne(u => u.Username == username);
            Console.WriteLine(loggedUser.Id);
            if (loggedUser != null)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "assets", image.FileName);
                FileStream fileStream = new(filePath, FileMode.Create);
                image.CopyTo(fileStream);
                note.Image = filePath;
                note.UserId = loggedUser.Id;
                if (_noteRepos.Save(note))
                {
                    NoteResponseDTO noteResponse = new()
                    {
                        Title = note.Title,
                        Description = note.Description,
                        Code = note.Code,
                        Image = note.Image
                    };
                    note.Codetags.ForEach(t =>
                    {
                        CodetagDTO tagResponse = new() { Name = t.Name };
                        noteResponse.Codetags.Add(tagResponse);
                    });
                    return Ok(noteResponse);
                }
                return BadRequest("Database error");

            }
            return Unauthorized("Logged user not found in database !");

            
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetNotes()
        {
            List<NoteResponseDTO> notesResponse = new();
            var userClaims = HttpContext.User;
            string username = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            UserApp loggedUser = _userRepos.SearchOne(u => u.Username == username);
            if (loggedUser != null)
            {
                _noteRepos.GetAllByUserId(loggedUser.Id).ForEach(n =>
                {
                    NoteResponseDTO noteResponse = new()
                    {
                        Title = n.Title,
                        Description = n.Description,
                        Code = n.Code,
                        Image = n.Code,
                    };
                    n.Codetags.ForEach(t =>
                    {
                        CodetagDTO tagResponse = new() { Name = t.Name };
                        noteResponse.Codetags.Add(tagResponse);
                    });

                    notesResponse.Add(noteResponse);
                });
                return Ok(notesResponse);
            }
            return Unauthorized();
        }
    }
}
