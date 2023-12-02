using Common.DataTransferObjects;
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
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        { 
            command.CommandText = string.Format("Select * from add_search_history(@userId, @searchQuery)");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new NpgsqlParameter("user_id", NpgsqlDbType.Varchar) { Value = userId });
            command.Parameters.Add(new NpgsqlParameter("search_string", NpgsqlDbType.Varchar) { Value = searchString });
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task<List<SearchResult>> StringSearch(string userId, string searchString)
    {
        var searchResults = new List<SearchResult>();

        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = string.Format(" Select * from string_search(@user_id, @search_string)");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new NpgsqlParameter("user_id", NpgsqlDbType.Varchar) { Value = userId });
            command.Parameters.Add(new NpgsqlParameter("search_string", NpgsqlDbType.Varchar) { Value = searchString });

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    searchResults.Add(new SearchResult(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2)));
                }
            }
        }

        return searchResults;
    }

    public async Task<List<SearchResult>> StructuredStringSearch(string userId, string title, string personName)
    {
        var searchResults = new List<SearchResult>();

        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = string.Format("Select * from structured_string_search(@user_id, @title, @person_name)");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new NpgsqlParameter("user_id", NpgsqlDbType.Varchar) { Value = userId });
            command.Parameters.Add(new NpgsqlParameter("title", NpgsqlDbType.Varchar) { Value = title });
            command.Parameters.Add(new NpgsqlParameter("person_name", NpgsqlDbType.Varchar) { Value = personName });

            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    searchResults.Add(new SearchResult(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2)));
                }
            }
            
        }

        return searchResults;
    }

    public async Task<float> CalculateTermFrequency(string tconst, string word)
    {
        // Implement your term frequency calculation logic
        throw new NotImplementedException();
    }

    public async Task<float> CalculateInverseDocumentFrequency(string word)
    {
        // Implement your inverse document frequency calculation logic
        throw new NotImplementedException();
    }
}