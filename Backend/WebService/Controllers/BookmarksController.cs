using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookmarksController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarksController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpPost("Movie")]
        public async Task<ActionResult> Create(BookmarkMovieDTO bookmarkMovie)
        {
            try
            {
                var result = await _bookmarkService.AddBookmarkMovies(bookmarkMovie.UserId,bookmarkMovie.AliasId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: BookmarksController/Create
        [HttpPost("Personality")]
        public async Task<IActionResult> Create(BookmarkPersonalityDTO bookmark)
        {
            try
            {
                var result = await _bookmarkService.AddBookmarkPersonality(bookmark.UserId,bookmark.PersonId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Movie")]
        public async Task<IActionResult> AddNote([FromQuery] string userId, [FromQuery] string aliasId, [FromQuery] string note)
        {
            try
            {
                var result = await _bookmarkService.AddNoteMovie(userId,aliasId,note);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("Personality")]
        public async Task<ActionResult> DeleteBookmarkPersonality([FromQuery] string userId, [FromQuery] string personId)
        {
            try
            {
                var result = await _bookmarkService.RemoveBookmarkPersonality(userId,personId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Movie")]
        public async Task<IActionResult> DeleteBookmarkMovie([FromQuery] string userId, [FromQuery] string aliasId)
        {
            try
            {
                var result = await _bookmarkService.RemoveBookmarkMovies(userId,aliasId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
