using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Contracts;

internal interface IMovieRepository<Movie> where Movie : class
{
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<Movie> GetByTitleAsync(string title);
    Task<Movie> Add(Movie movie);
    Task<bool> Update(Movie movie);
    Task<bool> DeleteAsync(string id);
}
