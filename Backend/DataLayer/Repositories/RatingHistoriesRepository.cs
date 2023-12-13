using Common.DataTransferObjects;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataLayer.Repositories;

public class RatingHistoriesRepository : IRatingHistoriesRepository
{
    private readonly AppDbContext _context;

    public RatingHistoriesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task RateMovie(string userId, string movieId, int rating)
    {
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = string.Format("Select * from add_rating(@userId, @movieId, @rating)");
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new NpgsqlParameter("userId", userId));
            command.Parameters.Add(new NpgsqlParameter("movieId", movieId));
            command.Parameters.Add(new NpgsqlParameter("rating", rating));
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<List<SimpleRatingHistory>> GetRatingHistory(string userId)
    {
        var ratingHistory = new List<SimpleRatingHistory>();

        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = string.Format("Select * from get_rating_history(@userId)");
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new NpgsqlParameter("userId", userId));

            await using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var history = new SimpleRatingHistory
                    {
                        MovieId = reader.GetString(0),
                        RatingValue = reader.GetInt32(1)
                    };
                    ratingHistory.Add(history);
                }
            }
            
        }

        return ratingHistory;
    }
}