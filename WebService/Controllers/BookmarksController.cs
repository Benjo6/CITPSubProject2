using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Http;
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
            if (await _bookmarkService.AddBookmarkMovies(bookmarkMovie.UserId, bookmarkMovie.AliasName))
            {
                return Ok();
            }
            else
            { 
                return BadRequest(); 
            }
        }

        // POST: BookmarksController/Create
        [HttpPost("Personality")]
        public async Task<ActionResult> Create(BookmarkPersonalityDTO bookmark)
        {
            if (await _bookmarkService.AddBookmarkPersonality(bookmark.UserId, bookmark.PersonId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("Movie")]
        public async Task<ActionResult> AddNote([FromQuery] string userId, [FromQuery] string aliasId, [FromQuery] string note)
        {
            if (await _bookmarkService.AddNoteMovie(userId, aliasId, note))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpDelete("Personality")]
        public async Task<ActionResult> DeleteBookmarkPersonality([FromQuery] string userId, [FromQuery] string personId)
        {
            if (await _bookmarkService.RemoveBookmarkPersonality(userId, personId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("Movie")]
        public async Task<ActionResult> DeleteBookmarkMovie([FromQuery] string userId, [FromQuery] string aliasId)
        {
            if (await _bookmarkService.RemoveBookmarkMovies(userId, aliasId))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
