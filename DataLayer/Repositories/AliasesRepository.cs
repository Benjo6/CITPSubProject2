using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories;

public class AliasesRepository : GenericRepository<Alias>, IAliasesRepository
{
    public AliasesRepository(AppDbContext context) : base(context)
    {
    }
}