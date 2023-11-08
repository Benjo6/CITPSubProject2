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
        using (var command = _context.Database.GetDbConnection().CreateCommand()) 
        {
            var query = "select * from find_actors_by_name(@name)";
            command.CommandText = query;
            command.Parameters.Add(new NpgsqlParameter("name", name));
            _context.Database.OpenConnection();

            using (var result = await command.ExecuteReaderAsync()) 
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
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            var query = "select * from find_actors_by_movie(@movieId)";
            command.CommandText = query;
            command.Parameters.Add(new NpgsqlParameter("movieId", movieId));
            _context.Database.OpenConnection();

            using (var result = await command.ExecuteReaderAsync())
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

    public async Task<List<PopularActor>> GetPopularActorsInMovie(string movieId)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            var query = "select * from getpopularactorsinmovie(@movieId)";
            command.CommandText = query;
            command.Parameters.Add(new NpgsqlParameter("movieId", movieId));
            _context.Database.OpenConnection();

            using (var result = await command.ExecuteReaderAsync())
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

    public async Task<List<PopularCoPlayer>> GetPopularCoPlayers(string actorName)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            var query = "select * from getpopularcoplayers(@actorName)";
            command.CommandText = query;
            command.Parameters.Add(new NpgsqlParameter("actorName", actorName));
            _context.Database.OpenConnection();

            using (var result = await command.ExecuteReaderAsync())
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
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            var query = "select * from person_words(@word, @frequency)";
            command.CommandText = query;
            command.Parameters.Add(new NpgsqlParameter("word", word));
            command.Parameters.Add(new NpgsqlParameter("frequency", frequency));
            _context.Database.OpenConnection();

            using (var result = await command.ExecuteReaderAsync())
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
}