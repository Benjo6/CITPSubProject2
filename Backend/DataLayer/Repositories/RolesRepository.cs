using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;
using Microsoft.Extensions.Logging;

namespace DataLayer.Repositories;

public class RolesRepository : GenericRepository<Role>, IRolesRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<RolesRepository> _logger;
    public RolesRepository(AppDbContext context, ILogger<RolesRepository> logger) : base(context)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddRoleAsync(string movieId, string personId, string title)
    {
        try
        {
            await _context.Roles.AddAsync(new Role { MovieId = movieId, PersonId = personId, Job = title });
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}