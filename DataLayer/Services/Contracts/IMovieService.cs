using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services.Contracts;

public interface IMovieService
{
    Task<Movie> GetMovieByTitleAsync(string title);
    Task<List<Movie>> GetAllAsync();
    Task<Movie> AddMovie( string type, string title, string original_title, bool isadult, DateTime? startyear, DateTime? endyear, int runtime, string genre, double rating, int votes);
    Task<bool> UpdateMovie(string id, string type, string title, string original_title, bool isadult, DateTime? startyear, DateTime? endyear, int runtime, string genre, double rating, int votes);
    Task<bool> DeleteMovie(string id);
}

