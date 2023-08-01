using Microsoft.AspNetCore.Mvc;
using noteCodeAPI.Services;

namespace noteCodeAPI.Controllers
{
    [Route("api/v1/tags")]
    [ApiController]
    public class CodetagController : ControllerBase
    {
        private CodetagService _codetagService;

        public CodetagController(CodetagService codetagService)
        {
            _codetagService = codetagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCodetagsAsync()
        {
            try
            {
                return Ok(await _codetagService.GetCodetagsAsync());
            } catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}
