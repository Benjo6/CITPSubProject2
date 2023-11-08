using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;
using DataLayer.Infrastructure;
using DataLayer.Repositories.Contracts;

namespace DataLayer.Repositories;

public class PeopleRepository : GenericRepository<Person>, IPeopleRepository
{
    public PeopleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<ActorBy>> FindActorsByName(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ActorBy>> FindActorsByMovie(string movieId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PopularActor>> GetPopularActorsInMovie(string movieId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PopularCoPlayer>> GetPopularCoPlayers(string actorName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<PersonWord>> PersonWords(string word, int frequency)
    {
        throw new NotImplementedException();
    }
}