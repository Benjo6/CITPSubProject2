using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Xml.Linq;

namespace DataLayer.Repositories;

public class PeopleRepository : GenericRepository<Person>, IPeopleRepository
{
    private readonly AppDbContext _context;
    public PeopleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ActorBy>> FindActorsByName(string name)
    {
        await using (var command = _context.Database.GetDbConnection().CreateCommand()) 
        {
            var query = "select * from find_actors_by_name(@name)";
            command.CommandText = query;
            command.Parameters.Add(new NpgsqlParameter("name", name));
            _context.Database.OpenConnection();

            await using (var result = await command.ExecuteReaderAsync()) 
            {
                var actors = new List<ActorBy>();

                while (await result.ReadAsync()) 
                {
                    var actorBy = new ActorBy
                    {
                        Id = result.GetString(result.GetOrdinal("id")),
                        Name = result.GetString(result.GetOrdinal("name"))
                    };

                    actors.Add(actorBy);
                }

                return actors;
            }
        }
    }

    public async Task<List<ActorBy>> FindActorsByMovie(string movieId)
    {
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            var query = "select * from find_actors_by_movie(@movieId)";
            command.CommandText = query;
            command.Parameters.Add(new NpgsqlParameter("movieId", movieId));
            _context.Database.OpenConnection();

            await using (var result = await command.ExecuteReaderAsync())
            {
                var actors = new List<ActorBy>();

                while (await result.ReadAsync())
                {
                    var actorBy = new ActorBy
                    {
                        Id = result.GetString(result.GetOrdinal("id")),
                        Name = result.GetString(result.GetOrdinal("name"))
                    };

                    actors.Add(actorBy);
                }

                return actors;
            }
        }
    }

    public async Task<List<PopularCoPlayer>> GetPopularCoPlayers(string actorName)
    {
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            var query = "select * from getpopularcoplayers(@actorName)";
            command.CommandText = query;
            command.Parameters.Add(new NpgsqlParameter("actorName", actorName));
            _context.Database.OpenConnection();

            await using (var result = await command.ExecuteReaderAsync())
            {
                var actors = new List<PopularCoPlayer>();

                while (await result.ReadAsync())
                {
                    var coPopularActor = new PopularCoPlayer
                    {
                        CoActorName = result.GetString(result.GetOrdinal("co_actor_name")),
                        AverageRating = result.GetDecimal(result.GetOrdinal("average_rating"))
                    };

                    actors.Add(coPopularActor);
                }

                return actors;
            }
        }
    }

    public async Task<List<PersonWord>> PersonWords(string word, int frequency)
    {
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            var query = "select * from person_words(@word, @frequency)";
            command.CommandText = query;
            command.Parameters.Add(new NpgsqlParameter("word", word));
            command.Parameters.Add(new NpgsqlParameter("frequency", frequency));
            _context.Database.OpenConnection();

            await using (var result = await command.ExecuteReaderAsync())
            {
                var actors = new List<PersonWord>();

                while (await result.ReadAsync())
                {
                    var personWord = new PersonWord
                    {
                        Word = result.GetString(result.GetOrdinal("word")),
                        Frequency = result.GetInt32(result.GetOrdinal("frequency"))
                    };

                    actors.Add(personWord);
                }

                return actors;
            }
        }
    }

    public async Task<List<SearchResult>> StringSearch(string searchString, int? resultCount)
    {
        // Define the SQL command text to call the string_search function
        const string commandText = "SELECT * FROM person_search(@searchString);";

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
        const string commandText = "SELECT * FROM person_search(@userId, @searchString)";
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
}