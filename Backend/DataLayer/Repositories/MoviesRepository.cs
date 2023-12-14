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
    
    public async Task<List<SearchResult>> StringSearch(string searchString, int? resultCount)
    {
        // Define the SQL command text to call the string_search function
        const string commandText = "SELECT * FROM string_search(@searchString);";

        // Create a parameter for the search string
        var searchStringParam = new NpgsqlParameter("@searchString", searchString);

        // Initialize a list to hold the search results
        var searchResults = new List<SearchResult>();

        // Use the existing database context to create a command
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = commandText;
            command.Parameters.Add(searchStringParam);

            // Open the database connection
            await _context.Database.OpenConnectionAsync();

            // Execute the command and obtain a data reader
            await using (var reader = await command.ExecuteReaderAsync())
            {
                var count = 0;
                while (await reader.ReadAsync() && (!resultCount.HasValue || count < resultCount.Value))
                {
                    // Extract the search result data from the current row
                    var id = reader.GetString(0);
                    var title = reader.GetString(1);
                    var rating = reader.GetDecimal(2); 
                    searchResults.Add(new SearchResult(id, title, rating));
                    count++;
                }
            }
        }
        return searchResults;
    }


    public async Task<List<SearchResult>> LoggedInStringSearch(string userId, string searchString, int? resultCount)
    {
        const string commandText = "SELECT * FROM string_search(@userId, @searchString)";
        var userIdParam = new NpgsqlParameter("@userId", userId);
        var searchStringParam = new NpgsqlParameter("@searchString", searchString);

        var searchResults = new List<SearchResult>();

        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = commandText;
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(searchStringParam);

            await _context.Database.OpenConnectionAsync();

            await using (var reader = await command.ExecuteReaderAsync())
            {
                var count = 0;
                while (await reader.ReadAsync() && (!resultCount.HasValue || count < resultCount.Value))
                {
                    var id = reader.GetString(0);
                    var title = reader.GetString(1);
                    var rating = reader.GetDecimal(2); 
                    searchResults.Add(new SearchResult(id, title, rating));
                    count++;
                }
            }
        }

        return searchResults;
    }


    public async Task<List<StructuredSearchResult>> StructuredStringSearch(string userId, string title, string personName, int? resultCount)
    {
        const string commandText = "SELECT * FROM structured_string_search(@userId, @title, @personName)";
        var userIdParam = new NpgsqlParameter("@userId", userId);
        var titleParam = new NpgsqlParameter("@title", title);
        var personNameParam = new NpgsqlParameter("@personName", personName);

        var searchResults = new List<StructuredSearchResult>();

        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = commandText;
            command.Parameters.Add(userIdParam);
            command.Parameters.Add(titleParam);
            command.Parameters.Add(personNameParam);

            await _context.Database.OpenConnectionAsync();

            await using (var reader = await command.ExecuteReaderAsync())
            {
                var count = 0;
                while (await reader.ReadAsync() && (!resultCount.HasValue || count < resultCount.Value))
                {
                    var id = reader.GetString(0);
                    var resultTitle = reader.GetString(1);
                    searchResults.Add(new StructuredSearchResult(id, resultTitle));
                    count++;
                }
            }

            await _context.Database.CloseConnectionAsync();
        }

        return searchResults;
    }
}