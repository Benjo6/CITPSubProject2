using DataLayer.Models;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository<Movie> _repository;

    public MovieService(IMovieRepository<Movie> repository)
    {
        _repository = repository;
    }

    public async Task<Movie> AddMovie(string type, string title, string original_title, bool isadult, DateTime? startyear, DateTime? endyear, int runtime, string genre, double rating, int votes)
    {
        var movie = new Movie { Type = type, Title = title, OriginalTitle=original_title, IsAdult=isadult,StartYear=startyear, EndYear=endyear, Runtime=runtime,Genre=genre, Rating=rating, Votes=votes };
        return await _repository.Add(movie);
    }

    public async Task<bool> DeleteMovie(string title)
    {
        if(await _repository.GetByTitleAsync(title) == null)
        {
            return false;
        }
        return await _repository.DeleteAsync(title);
    }

    public async Task<List<Movie>> GetAllAsync()
    {
        return (List<Movie>)await _repository.GetAllAsync();
    }

    public async Task<Movie> GetMovieByTitleAsync(string title)
    {
        return await _repository.GetByTitleAsync(title);
    }

    public async Task<bool> UpdateMovie(string id, string type, string title, string original_title, bool isadult, DateTime? startyear, DateTime? endyear, int runtime, string genre, double rating, int votes)
    {
        var movie = await _repository.GetByTitleAsync(title);
        if (movie == null)
        {
            return false;
        }
        movie.Type = type;
        movie.Title = title;
        movie.OriginalTitle = original_title;
        movie.IsAdult= isadult;
        movie.StartYear = startyear;
        movie.EndYear = endyear;
        movie.Runtime = runtime;
        movie.Genre = genre;
        movie.Rating = rating;
        movie.Votes = votes;

        return await _repository.Update(movie);
    }
}
