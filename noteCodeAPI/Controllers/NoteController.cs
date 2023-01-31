using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using noteCodeAPI.DTOs;
using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using System.Security.Claims;

namespace noteCodeAPI.Controllers
{
    [Route("api/v1/{userId}/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private NoteRepository _noteRepos;
        private UserAppRepository _userRepos;

        public NoteController(NoteRepository noteRepos, UserAppRepository userRepos)
        {
            _noteRepos = noteRepos;
            _userRepos = userRepos;
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostNote(int userId, [FromBody] NoteRequestDTO noteRequest, IFormFile image)
        {
            Note note = new Note()
            {
                Title = noteRequest.Title,
                Description = noteRequest.Description,
                Code = noteRequest.Code,
                Tags = noteRequest.Tags
            };
            var userClaims = HttpContext.User;
            string username = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            UserApp loggedUser = _userRepos.SearchOne(u => u.Username == username);
            if (loggedUser != null)
            {
                userId= loggedUser.Id;
            }
            if (image == null || image.Length== 0 )
            {
                return BadRequest("No file found.");
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "assets")

            try
            {
                _noteRepos.Save(note);
                return Ok(_noteRepos.GetAll().Last());
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "Erreur serveur"});
            };
        }
    }
}
