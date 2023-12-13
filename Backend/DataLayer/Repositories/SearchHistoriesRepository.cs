using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace DataLayer.Repositories;

public class SearchHistoriesRepository : GenericRepository<SearchHistory>, ISearchHistoriesRepository
{
    private readonly AppDbContext _context;
    public SearchHistoriesRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddSearchHistory(string userId, string searchString)
    {
        await using (var command = _context.Database.GetDbConnection().CreateCommand())
        { 
            command.CommandText = string.Format("Select * from add_search_history(@userId, @searchQuery)");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new NpgsqlParameter("user_id", NpgsqlDbType.Varchar) { Value = userId });
            command.Parameters.Add(new NpgsqlParameter("search_string", NpgsqlDbType.Varchar) { Value = searchString });
            await command.ExecuteNonQueryAsync();
        }
    }

}

