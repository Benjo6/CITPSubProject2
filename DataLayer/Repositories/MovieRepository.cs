using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories;

internal class MovieRepository<Movie> : IMovieRepository<Movie> where Movie : class
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger _logger;

    public MovieRepository(AppDbContext appDbContext, ILogger logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }
    public async Task<Movie> Add(Movie movie)
    {
        _appDbContext.Set<Movie>().Add(movie);
        await _appDbContext.SaveChangesAsync();
        return movie;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var movie = await GetByTitleAsync(id);

        if (movie != null) { return false; }

        _appDbContext.Set<Movie>().Remove(movie);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _appDbContext.Set<Movie>().ToListAsync();
    }

    public async Task<Movie> GetByTitleAsync(string title)
    {
        return await _appDbContext.Set<Movie>().FindAsync(title);
    }

    public async Task<bool> Update(Movie movie)
    {
        _appDbContext.Entry(movie).State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}
