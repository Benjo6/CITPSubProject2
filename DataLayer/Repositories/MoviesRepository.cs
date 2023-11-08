using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class RatingHistoriesRepository : IRatingHistoriesRepository
    {
        private readonly AppDbContext _context;

        public RatingHistoriesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task RateMovie(string userId, string movieId, int rating)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT rate(@userId, @movieId, @rating)";
                command.Parameters.Add(new NpgsqlParameter("userId", NpgsqlDbType.Varchar) { Value = userId });
                command.Parameters.Add(new NpgsqlParameter("movieId", NpgsqlDbType.Varchar) { Value = movieId });
                command.Parameters.Add(new NpgsqlParameter("rating", NpgsqlDbType.Integer) { Value = rating });

                _context.Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<SimpleRatingHistory>> GetRatingHistory(string userId)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_rating_history(@userId)";
                command.Parameters.Add(new NpgsqlParameter("userId", NpgsqlDbType.Varchar) { Value = userId });

                _context.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    var ratingHistory = new List<SimpleRatingHistory>();

                    while (await result.ReadAsync())
                    {
                        var history = new SimpleRatingHistory
                        {
                            MovieId = result.GetString(result.GetOrdinal("movie_id")),
                            RatingValue = result.GetInt32(result.GetOrdinal("rating_value"))
                        };
                        ratingHistory.Add(history);
                    }

                    return ratingHistory;
                }
            }
        }
    }
}
