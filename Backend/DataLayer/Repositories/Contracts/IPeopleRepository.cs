using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Generics;

namespace DataLayer.Repositories.Contracts;

public interface IPeopleRepository : IGenericRepository<Person>
{
    public Task<List<ActorBy>> FindActorsByName(string name);
    public Task<List<ActorBy>> FindActorsByMovie(string movieId);
    public Task<List<PopularCoPlayer>> GetPopularCoPlayers(string actorName);
    public Task<List<PersonWord>> PersonWords(string word, int frequency);
    

}