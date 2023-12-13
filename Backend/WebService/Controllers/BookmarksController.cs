using System;
using System.Threading.Tasks;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Exception = System.Exception;

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
        
        // GET: Bookmarks/Movie/
        [HttpGet("Movie")]
        public async Task<IActionResult> GetMovies(
            string userId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _bookmarkService.GetBookmarkMovies(userId,page,pageSize);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
        
        // GET: Bookmarks/Personality/
        [HttpGet("Personality")]
        public async Task<IActionResult> GetPerson(
            string userId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _bookmarkService.GetBookmarkPersons(userId,page,pageSize);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        // POST: Bookmarks/Movie
        [HttpPost("Movie")]
        public async Task<ActionResult> CreateBMMovie(string userId, string aliasId)
        {
            try
            {
                var result = await _bookmarkService.AddBookmarkMovies(userId,aliasId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        // POST: Bookmarks/Personality
        [HttpPost("Personality")]
        public async Task<ActionResult> CreateBMPerson(string userId, string personId)
        {
            try
            {
                var result = await _bookmarkService.AddBookmarkPersonality(userId, personId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
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
                return BadRequest(new {message = ex.Message});
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
                return BadRequest(new {message = ex.Message});
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
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}
