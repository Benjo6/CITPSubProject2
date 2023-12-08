using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers;

[Route("[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _service;

    public MoviesController(IMoviesService service)
    {
        _service = service;
    }

    // GET: Movies
    [HttpGet]
    public async Task<IActionResult> GetMovies(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Dictionary<string, string>? filterCriteria = null,
        [FromQuery] string sortBy = "Id",
        [FromQuery] bool asc = true)
    {
        try
        {
            var (movies, metadata) = await _service.GetAllMovies(new Filter(page, pageSize, sortBy, asc, filterCriteria));
            return Ok(new{movies,metadata});
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: Movies/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(string id)
    {
        try
        {
            var movie = await _service.GetOneMovie(id);
            return Ok(movie);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
        }
    }

    // GET: Movies/FindSimilarMovies/13
    [HttpGet("FindSimilarMovies")]
    public async Task<IActionResult> FindSimilarMovies([FromQuery] string movieId)
    {
        try
        {
            var similarMovies = await _service.FindSimilarMovies(movieId);
            return Ok(similarMovies);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: Movies/WordToWord?keywords=Action&keywords=Comedy
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
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Movies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(string id, AlterMovieDTO movie)
    {
        try
        {
            var putMovie = await _service.UpdateMovie(id, movie);
            return Ok(putMovie);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/Movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostMovie([FromBody] AlterMovieDTO movie)
    {
        try
        {
            var postMovie = await _service.AddMovie(movie);
            return Ok(postMovie);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(string id)
    {
        try
        {
            var result = await _service.DeleteMovie(id);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}