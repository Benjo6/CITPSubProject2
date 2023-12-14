using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        [FromQuery] Dictionary<string,string>? conditions = null,
        [FromQuery] string sortBy = "Id",
        [FromQuery] bool asc = true)
    {
        try
        {
            var searchHistories = await _service.GetAllSearchHistory(new Filter(page,pageSize,sortBy,asc,conditions));
            return Ok(searchHistories);
        }
        catch(Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    // GET: Search/History/5
    [HttpGet("History/{id}")]
    public async Task<IActionResult> GetOneSearchHistory(string id)
    {
        try
        {
            var searchHistory = await _service.GetOneSearchHistory(id);
            return Ok(searchHistory);

        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }
    
    // GET: Search/WordToWord?keywords=Action&keywords=Comedy
    [HttpGet("WordToWords")]
    public async Task<IActionResult> WordToWordsQuery([FromQuery] string[] keywords)
    {
        try
        {
            var wordToWord = await _service.WordToWordsQuery(keywords);
            return Ok(wordToWord);

        }
        catch (Exception ex)
        { 
            return BadRequest(new {message = ex.Message});
        }
    }
    
    // GET: Movies/BestMatchQuery?keywords=Action&keywords=Comedy
    [HttpGet("BestMatchQuery")]
    public async Task<IActionResult> BestMatchQuery([FromQuery] string[] keywords)
    {
        try
        {
            var bestMatchQuery = await _service.BestMatchQuery(keywords);
            return Ok(bestMatchQuery);

        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    // GET: Movies/ExactMatch?keywords=Action&keywords=Comedy
    [HttpGet("ExactMatch")]
    public async Task<IActionResult> ExactMatchQuery([FromQuery] string[] keywords)
    {
        try
        {
            var exactMatchQuery = await _service.ExactMatchQuery(keywords);
            return Ok(exactMatchQuery);

        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }
    
    [HttpGet("ActorWords")]
    public async Task<IActionResult> PersonWords(string word, int frequency)
    {
        try
        {
            var actors = await _service.PersonWords(word, frequency);
            return Ok(actors);
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Search(string searchString, int? resultCount = 10)
    {
        try
        {
            var result = await _service.StringSearch(searchString, resultCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }
    
    [HttpGet("LoggedIn")]
    public async Task<IActionResult> LoggedInSearch(string userId, string searchString, int? resultCount = 10)
    {
        try
        {
            var result = await _service.LoggedInStringSearch(userId,searchString, resultCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }
    
    [HttpGet("Structured")]
    public async Task<IActionResult> StructuredSearch(string userId, string title, string personName, int? resultCount = 10)
    {
        try
        {
            var result = await _service.StructuredStringSearch(userId, title, personName, resultCount);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }
    // string_search
    
    // structured_string_search
    
}