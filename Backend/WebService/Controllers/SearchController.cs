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
            var (searchHistories, metadata) = await _service.GetAllSearchHistory(new Filter(page,pageSize,sortBy,asc,conditions));
            return Ok(new {searchHistories,metadata});
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