using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories;

public class RolesRepository : GenericRepository<Role>, IRolesRepository
{
    public RolesRepository(AppDbContext context) : base(context)
    {
    }
}