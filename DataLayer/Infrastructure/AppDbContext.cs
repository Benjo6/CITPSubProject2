using Common.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Remove this in the future, it's just for testing
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
