using Microsoft.AspNetCore.Mvc;
using NoteOrganizer.Core.DTO;
using NoteOrganizer.Core.Interface;
using NoteOrganizer.Core.Services;

namespace NoteOrganizer.Controllers
{
    [ApiController]
    [Route("api/Note/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        [HttpPost]
        [Route("CreateNoteById")]
        public async Task<IActionResult> Create([FromBody] NoteDto note, [FromHeader] string AccessToken, [FromHeader] string UserId)
        {
            var result = await _noteService.CreateNoteById(note);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("UpdateNoteById")]
        public async Task<IActionResult> update([FromBody] NoteDto noteId, [FromHeader] string AccessToken, [FromHeader] string UserId)
        {
            var response = await _noteService.UpdateNoteById(noteId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Route("DeleteNoteById")]
        public async Task<IActionResult> Delete([FromBody] string noteId, [FromHeader] string AccessToken, [FromHeader] string UserId)
        {
            var response = await _noteService.DeleteNoteById(noteId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        [Route("GetNotesById")]
        public async Task<IActionResult> Get([FromBody] string noteId, [FromHeader] string AccessToken, [FromHeader] string UserId)
        {
            var response = await _noteService.GetNotesById(noteId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        [Route("GetNotesByUserId")]
        public async Task<IActionResult> GetAll( [FromHeader] string AccessToken, [FromHeader] string UserId)
        {
            var response = await _noteService.GetNotesById(UserId);
            return StatusCode(response.StatusCode, response);
        }
    };
}