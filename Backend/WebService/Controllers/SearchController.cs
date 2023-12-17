using Common;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[Route("[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly ISearchService _service;
    public SearchController(ISearchService service)
    {
        _service = service;
    }

    // GET: Search/History
    [HttpGet("History")]
    public async Task<IActionResult> GetSearchHistories(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Dictionary<string, string>? conditions = null,
        [FromQuery] string sortBy = "Id",
        [FromQuery] bool asc = true)
    {
        try
        {
            var searchHistories = await _service.GetAllSearchHistory(new Filter(page, pageSize, sortBy, asc, conditions));
            var listUri = Url.Action("GetSearchHistories", new { page = page, pageSize = pageSize, conditions = conditions, sortBy = sortBy, asc = asc });
            return Ok(new { searchHistories = searchHistories, listUri = listUri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: Search/History/5
    [HttpGet("History/{id}")]
    public async Task<IActionResult> GetOneSearchHistory(string id)
    {
        try
        {
            var searchHistory = await _service.GetOneSearchHistory(id);
            var uri = Url.Action("GetOneSearchHistory", new { id = id });
            return Ok(new { searchHistory = searchHistory, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: Search/WordToWord?keywords=Action&keywords=Comedy
    [HttpGet("WordToWords")]
    public async Task<IActionResult> WordToWordsQuery([FromQuery] string[] keywords)
    {
        try
        {
            var wordToWord = await _service.WordToWordsQuery(keywords);
            var uri = Url.Action("WordToWordsQuery", new { keywords = keywords });
            return Ok(new { wordToWord = wordToWord, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: Movies/BestMatchQuery?keywords=Action&keywords=Comedy
    [HttpGet("BestMatchQuery")]
    public async Task<IActionResult> BestMatchQuery([FromQuery] string[] keywords)
    {
        try
        {
            var bestMatchQuery = await _service.BestMatchQuery(keywords);
            var uri = Url.Action("BestMatchQuery", new { keywords = keywords });
            return Ok(new { bestMatchQuery = bestMatchQuery, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: Movies/ExactMatch?keywords=Action&keywords=Comedy
    [HttpGet("ExactMatch")]
    public async Task<IActionResult> ExactMatchQuery([FromQuery] string[] keywords)
    {
        try
        {
            var exactMatchQuery = await _service.ExactMatchQuery(keywords);
            var uri = Url.Action("ExactMatchQuery", new { keywords = keywords });
            return Ok(new { exactMatchQuery = exactMatchQuery, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("ActorWords")]
    public async Task<IActionResult> PersonWords(string word, int frequency)
    {
        try
        {
            var actors = await _service.PersonWords(word, frequency);
            var uri = Url.Action("PersonWords", new { word = word, frequency = frequency });
            return Ok(new { actors = actors, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("Movie")]
    public async Task<IActionResult> MovieSearch(string searchString, int? resultCount = 10)
    {
        try
        {
            var result = await _service.MovieSearch(searchString, resultCount);
            var uri = Url.Action("MovieSearch", new { searchString = searchString, resultCount = resultCount });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("Movie/LoggedIn")]
    public async Task<IActionResult> LoggedInMovieSearch(string userId, string searchString, int? resultCount = 10)
    {
        try
        {
            var result = await _service.LoggedInMovieSearch(userId, searchString, resultCount);
            var uri = Url.Action("LoggedInMovieSearch", new { userId = userId, searchString = searchString, resultCount = resultCount });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("Person")]
    public async Task<IActionResult> PersonSearch(string searchString, int? resultCount = 10)
    {
        try
        {
            var result = await _service.PersonSearch(searchString, resultCount);
            var uri = Url.Action("PersonSearch", new { searchString = searchString, resultCount = resultCount });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("Person/LoggedIn")]
    public async Task<IActionResult> LoggedInPersonSearch(string userId, string searchString, int? resultCount = 10)
    {
        try
        {
            var result = await _service.LoggedInPersonSearch(userId, searchString, resultCount);
            var uri = Url.Action("LoggedInPersonSearch", new { userId = userId, searchString = searchString, resultCount = resultCount });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("Structured")]
    public async Task<IActionResult> StructuredSearch(string userId, string title, string personName, int? resultCount = 10)
    {
        try
        {
            var result = await _service.StructuredStringSearch(userId, title, personName, resultCount);
            var uri = Url.Action("StructuredSearch", new { userId = userId, title = title, personName = personName, resultCount = resultCount });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
