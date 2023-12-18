using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Exception = System.Exception;

namespace WebService.Controllers;

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
    [Authorize]
    public async Task<IActionResult> GetMovies(
        string userId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _bookmarkService.GetBookmarkMovies(userId, page, pageSize);
            var uri = Url.Action("GetMovies", new { userId = userId, page = page, pageSize = pageSize });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: Bookmarks/Personality/
    [HttpGet("Personality")]
    [Authorize]
    public async Task<IActionResult> GetPerson(
        string userId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _bookmarkService.GetBookmarkPersons(userId, page, pageSize);
            var uri = Url.Action("GetPerson", new { userId = userId, page = page, pageSize = pageSize });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST: Bookmarks/Movie
    [HttpPost("Movie")]
    [Authorize]
    public async Task<ActionResult> CreateBMMovie(string userId, string movieId)
    {
        try
        {
            var result = await _bookmarkService.AddBookmarkMovies(userId, movieId);
            var uri = Url.Action("CreateBMMovie", new { userId = userId, movieId = movieId });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST: Bookmarks/Personality
    [HttpPost("Personality")]
    [Authorize]
    public async Task<ActionResult> CreateBMPerson(string userId, string personId)
    {
        try
        {
            var result = await _bookmarkService.AddBookmarkPersonality(userId, personId);
            var uri = Url.Action("CreateBMPerson", new { userId = userId, personId = personId });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("Movie")]
    [Authorize]
    public async Task<IActionResult> AddNote([FromQuery] string userId, [FromQuery] string movieId, [FromQuery] string note)
    {
        try
        {
            var result = await _bookmarkService.AddNoteMovie(userId, movieId, note);
            var uri = Url.Action("AddNote", new { userId = userId, movieId = movieId, note = note });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpDelete("Personality")]
    [Authorize]
    public async Task<ActionResult> DeleteBookmarkPersonality([FromQuery] string userId, [FromQuery] string personId)
    {
        try
        {
            var result = await _bookmarkService.RemoveBookmarkPersonality(userId, personId);
            var uri = Url.Action("DeleteBookmarkPersonality", new { userId = userId, personId = personId });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpDelete("Movie")]
    [Authorize]
    public async Task<IActionResult> DeleteBookmarkMovie([FromQuery] string userId, [FromQuery] string movieId)
    {
        try
        {
            var result = await _bookmarkService.RemoveBookmarkMovies(userId, movieId);
            var uri = Url.Action("DeleteBookmarkMovie", new { userId = userId, movieId = movieId });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

