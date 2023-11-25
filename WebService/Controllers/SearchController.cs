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
    public async Task<IActionResult> GetSearchHistories([FromQuery] Filter? filter = null)
    {
        filter ??= new Filter();
        try
        {
            var searchHistories = await _service.GetAllSearchHistory(filter);
            return Ok(searchHistories);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
        }
    }
}