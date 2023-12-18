using Common;
using Common.DataTransferObjects;
using Common.Identity;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
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
            var movies = await _service.GetAllMovies(new Filter(page, pageSize, sortBy, asc, filterCriteria));
            var listUri = Url.Action("GetMovies", new { page = page, pageSize = pageSize, filterCriteria = filterCriteria, sortBy = sortBy, asc = asc });
            var moviesWithUris = movies.Select(m => new
            {
                movie = m,
                uri = Url.Action("GetMovie", new { id = m.Id })
            });

            return Ok(new { movies = moviesWithUris, uri = listUri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    // GET: Movies/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(string id)
    {
        try
        {
            var movie = await _service.GetOneMovie(id);
            var uri = Url.Action("GetMovie", new { id = id });
            return Ok(new { movie = movie, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: Movies/PopularActor/1id1
    [HttpGet("PopularActor")]
    public async Task<IActionResult> GetPopularActorsInMovie([FromQuery] string movieId)
    {
        try
        {
            var actors = await _service.GetPopularActorsInMovie(movieId);
            var uri = Url.Action("GetPopularActorsInMovie", new { movieId = movieId });
            return Ok(new { actors = actors, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // GET: Movies/FindSimilarMovies/1id1
    [HttpGet("FindSimilarMovies")]
    public async Task<IActionResult> FindSimilarMovies([FromQuery] string movieId)
    {
        try
        {
            var similarMovies = await _service.FindSimilarMovies(movieId);
            var uri = Url.Action("FindSimilarMovies", new { movieId = movieId });
            return Ok(new { similarMovies = similarMovies, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT: api/Movies/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IActionResult> PutMovie(string id, AlterMovieDTO movie)
    {
        try
        {
            var putMovie = await _service.UpdateMovie(id, movie);
            var uri = Url.Action("PutMovie", new { id = id });
            return Ok(new { movie = putMovie, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // Rate
    [HttpPut("Rate")]
    public async Task<IActionResult> Rate(string userId, string movieId, decimal rating)
    {
        try
        {
            var putMovie = await _service.RateMovie(userId, movieId, rating);
            var uri = Url.Action("Rate", new { userId = userId, movieId = movieId, rating = rating });
            return Ok(new { movie = putMovie, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST: api/Movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IActionResult> PostMovie([FromBody] AlterMovieDTO movie)
    {
        try
        {
            var postMovie = await _service.AddMovie(movie);
            var uri = Url.Action("PostMovie");
            return Ok(new { movie = postMovie, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    [Authorize(Policy = IdentityData.AdminUserPolicyName)]
    public async Task<IActionResult> DeleteMovie(string id)
    {
        try
        {
            var result = await _service.DeleteMovie(id);
            var uri = Url.Action("DeleteMovie", new { id = id });
            return Ok(new { result = result, uri = uri });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
