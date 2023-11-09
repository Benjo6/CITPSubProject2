using Npgsql;
using NpgsqlTypes;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class BookmarkMoviesRepository : IBookmarkMoviesRepository
    {
        private readonly AppDbContext context;

        public BookmarkMoviesRepository(AppDbContext _context)
        {
            context = _context;
        }

        public async Task AddBookmarkMovies(string userId, string aliasId)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = string.Format("Select * from add_bookmark_movie(@user_id, @alias_id)");
                command.Parameters.Add(new NpgsqlParameter("user_id", NpgsqlDbType.Varchar) { Value = userId });
                command.Parameters.Add(new NpgsqlParameter("alias_id", NpgsqlDbType.Varchar) { Value = aliasId });
                await command.ExecuteNonQueryAsync();
                
            }
        }

        public async Task<List<string>> GetBookmarkMovies(string userId)
        {
            var bookmarkedMovies = new List<string>();

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = string.Format("Select * from get_bookmarks_movie(@user_id");
                    command.Parameters.Add(new NpgsqlParameter("userId", NpgsqlDbType.Varchar) { Value = userId });

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            bookmarkedMovies.Add(reader.GetString(0));
                        }
                    }
            }

            return bookmarkedMovies;
        }

        public async Task AddNote(string userId, string aliasId, string note)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = string.Format("Select * from add_note(@userId, @aliasId, @note)");
                command.Parameters.Add(new NpgsqlParameter("userId", NpgsqlDbType.Varchar) { Value = userId });
                command.Parameters.Add(new NpgsqlParameter("aliasId", NpgsqlDbType.Varchar) { Value = aliasId });
                command.Parameters.Add(new NpgsqlParameter("note", NpgsqlDbType.Text) { Value = note });
                await command.ExecuteNonQueryAsync();
                
            }
        }

        public async Task<bool> DeleteBookmarkMovie(string userId, string aliasId)
        {
            var bookmarkToRemove = context.BookmarkMovies.FirstOrDefault(x => x.AliasId == aliasId && x.UserId == userId);

            if (bookmarkToRemove != null)
            {
                context.BookmarkMovies.Remove(bookmarkToRemove);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
