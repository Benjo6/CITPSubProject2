using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Identity;
using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.Services.Contracts;
using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace WebService.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly AppDbContext _appDbContext;

    public MovieController(AppDbContext appDbContext) {  _appDbContext = appDbContext; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
    {
        try
        {
            if (_appDbContext.Movies== null) 
            {
                return NotFound();
            }
            return await _appDbContext.Movies.ToListAsync();
        }
        catch (Exception ex) { return BadRequest(ex.Message); }
    }
        
}
