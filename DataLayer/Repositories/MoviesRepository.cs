using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Npgsql;


namespace DataLayer.Repositories;

public class MoviesRepository : GenericRepository<Movie>, IMoviesRepository
{
    private readonly AppDbContext _context;

    public MoviesRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<string>> ExactMatchQuery(string[] keywords)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "select * from exact_match_query(VARIADIC @keywords::varchar[])";
            command.Parameters.Add(new NpgsqlParameter("keywords", keywords));
            _context.Database.OpenConnection();

            using (var result = await command.ExecuteReaderAsync())
            {
                var exactMatches = new List<string>();

                while (await result.ReadAsync())
                {
                    var tconst = result.GetString(result.GetOrdinal("tconst"));
                    exactMatches.Add(tconst);
                }

                return exactMatches;
            }
        }
    }

    public async Task<List<BestMatch>> BestMatchQuery(string[] keywords)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "select * from best_match_query(VARIADIC @keywords::varchar[])";
            command.Parameters.Add(new NpgsqlParameter("keywords", keywords));
            _context.Database.OpenConnection();

            using (var result = await command.ExecuteReaderAsync())
            {
                var bestMatches = new List<BestMatch>();

                while (await result.ReadAsync())
                {
                    var bestMatch = new BestMatch
                    {
                        Rank = result.GetInt32(result.GetOrdinal("Rank")),
                        Tconst = result.GetString(result.GetOrdinal("Tconst"))
                    };

                    bestMatches.Add(bestMatch);
                }

                return bestMatches;
            }
        }
    }

    public async Task<List<WordFrequency>> WordToWordsQuery(string[] keywords)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "select * from word_to_words_query(VARIADIC @keywords::varchar[])";
            command.Parameters.Add(new NpgsqlParameter("keywords", keywords));
            _context.Database.OpenConnection();

            using (var result = await command.ExecuteReaderAsync())
            {
                var wordFrequencies = new List<WordFrequency>();

                while (await result.ReadAsync())
                {
                    var wordFrequency = new WordFrequency
                    {
                        Word = result.GetString(result.GetOrdinal("Word")),
                        Frequency = result.GetInt32(result.GetOrdinal("Frequency"))
                    };

                    wordFrequencies.Add(wordFrequency);
                }

                return wordFrequencies;
            }
        }
    }

    public async Task<List<SimilarMovie>> FindSimilarMovies(string movieId)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "select * from find_similar_movies(@movieId)";
            command.Parameters.Add(new NpgsqlParameter("movieId", movieId));
            _context.Database.OpenConnection();

            using (var result = await command.ExecuteReaderAsync())
            {
                var similarMovies = new List<SimilarMovie>();

                while (await result.ReadAsync())
                {
                    var similarMovie = new SimilarMovie
                    {
                        Id = result.GetString(result.GetOrdinal("similar_movie_id")),
                        Title = result.GetString(result.GetOrdinal("similar_movie_title"))
                    };

                    similarMovies.Add(similarMovie);
                }

                return similarMovies;
            }
        }
    }
}