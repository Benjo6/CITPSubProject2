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

    public async Task<List<string>> ExactMatchQuery(string[] keywords, int? page = 1, int? perPage = 10)
    {
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "select * from exact_match_query(VARIADIC @keywords::varchar[])";
            command.Parameters.Add(new NpgsqlParameter("keywords", keywords));
            _context.Database.OpenConnection();

            await using (var result = await command.ExecuteReaderAsync())
            {
                var exactMatches = new List<string>();

                var count = 0;
                while (await result.ReadAsync() && count <= page * perPage)
                {
                    if (count < (page - 1) * perPage)
                    {
                        count++;
                        continue;
                    }
                    count++;
                    var tconst = result.GetString(result.GetOrdinal("tconst"));
                    exactMatches.Add(tconst);
                }

                return exactMatches;
            }
        }
    }

    public async Task<List<BestMatch>> BestMatchQuery(string[] keywords, int? page = 1, int? perPage = 10)
    {
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "select * from best_match_query(VARIADIC @keywords::varchar[])";
            command.Parameters.Add(new NpgsqlParameter("keywords", keywords));
            _context.Database.OpenConnection();

            await using (var result = await command.ExecuteReaderAsync())
            {
                var bestMatches = new List<BestMatch>();
                var count = 0;
                while (await result.ReadAsync() && count <= page * perPage)
                {
                    if (count < (page - 1) * perPage)
                    {
                        count++;
                        continue;
                    }
                    count++;
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
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "select * from word_to_words_query(VARIADIC @keywords::varchar[])";
            command.Parameters.Add(new NpgsqlParameter("keywords", keywords));
            await _context.Database.OpenConnectionAsync();

            await using (var result = await command.ExecuteReaderAsync())
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

    public async Task<List<SimilarMovie>> FindSimilarMovies(string movieId, int? page = 1, int? perPage = 10)
    {
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "select * from find_similar_movies(@movieId)";
            command.Parameters.Add(new NpgsqlParameter("movieId", movieId));
            await _context.Database.OpenConnectionAsync();

            await using (var result = await command.ExecuteReaderAsync())
            {
                var similarMovies = new List<SimilarMovie>();

                var count = 0;
                while (await result.ReadAsync() && count <= page * perPage)
                {
                    if (count < (page - 1) * perPage)
                    {
                        count++;
                        continue;
                    }
                    count++;
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
    
    public async Task<List<PopularActor>> GetPopularActorsInMovie(string movieId)
    {
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            var query = "select * from getpopularactorsinmovie(@movieId)";
            command.CommandText = query;
            command.Parameters.Add(new NpgsqlParameter("movieId", movieId));
            await _context.Database.OpenConnectionAsync();

            await using (var result = await command.ExecuteReaderAsync())
            {
                var actors = new List<PopularActor>();

                while (await result.ReadAsync())
                {
                    var popularActor = new PopularActor
                    {
                        ActorName = result.GetString(result.GetOrdinal("actor_name")),
                        AverageRating = result.GetDecimal(result.GetOrdinal("average_rating"))
                    };

                    actors.Add(popularActor);
                }

                return actors;
            }
        }
    }

    public async Task RateMovie(string userId, string movieId, decimal rating)
    {
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT rate(@userId, @movieId, @rating)";
            command.Parameters.Add(new NpgsqlParameter("userId", userId));
            command.Parameters.Add(new NpgsqlParameter("movieId", movieId));
            command.Parameters.Add(new NpgsqlParameter("rating", rating));
            await _context.Database.OpenConnectionAsync();

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<List<SearchResults>> StringSearch(string userId, string searchString)
    {
        var searchResults = new List<SearchResults>();
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT * FROM string_search(@userId, @searchString)";
            command.Parameters.Add(new NpgsqlParameter("userId", userId));
            command.Parameters.Add(new NpgsqlParameter("searchString", searchString));
            await _context.Database.OpenConnectionAsync();

            await using (var result = await command.ExecuteReaderAsync())
            {
                while (await result.ReadAsync())
                {
                    searchResults.Add(new SearchResults
                    {
                        Id = result.GetString(result.GetOrdinal("id")),
                        Title = result.GetString(result.GetOrdinal("title"))
                    });
                }
            }
        }
        return searchResults;
    }
    
    public async Task<List<SearchResults>> StructuredStringSearch(string userId, string title, string personName)
    {
        var searchResults = new List<SearchResults>();
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT * FROM structured_string_search(@userId, @title, @personName)";
            command.Parameters.Add(new NpgsqlParameter("userId", userId));
            command.Parameters.Add(new NpgsqlParameter("title", title));
            command.Parameters.Add(new NpgsqlParameter("personName", personName));
            await _context.Database.OpenConnectionAsync();

            await using (var result = await command.ExecuteReaderAsync())
            {
                while (await result.ReadAsync())
                {
                    searchResults.Add(new SearchResults
                    {
                        Id = result.GetString(result.GetOrdinal("id")),
                        Title = result.GetString(result.GetOrdinal("title"))
                    });
                }
            }
        }
        return searchResults;
    }

}