using Common;
using Common.DataTransferObjects;

namespace DataLayer.Services.Contracts;

public interface IPeopleService
{
    Task<(List<GetAllPersonDTO>, Metadata)> GetAllPerson(Filter filter);
    Task<GetOnePersonDTO> GetOnePerson(string id);
    Task<UpdatePersonDTO> UpdatePerson(string id, AlterPersonDTO person);
    Task<UpdatePersonDTO> AddPerson(AlterPersonDTO person);
    Task<bool> DeletePerson(string id);
    public Task<List<ActorBy>> FindActorsByName(string name);
    public Task<List<ActorBy>> FindActorsByMovie(string movieId);
    public Task<List<PopularCoPlayer>> GetPopularCoPlayers(string actorName);
}