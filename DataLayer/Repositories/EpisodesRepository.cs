using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories;

public class EpisodesRepository : GenericRepository<Episode>, IEpisodesRepository
{
    public EpisodesRepository(AppDbContext context) : base(context)
    {
    }
}