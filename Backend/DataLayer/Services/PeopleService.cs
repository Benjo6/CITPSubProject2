using Common;
using Common.DataTransferObjects;
using Common.Mapper;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class PeopleService : IPeopleService
{
    private readonly IPeopleRepository _peopleRepository;
    private readonly ObjectMapper _mapper;

    public PeopleService(IPeopleRepository peopleRepository)
    {
        _peopleRepository = peopleRepository;
        _mapper = new ObjectMapper();
    }

    public async Task<List<GetAllPersonDTO>> GetAllPerson(Filter filter)
    {
        var getAll = await _peopleRepository.GetAll(filter);
        return _mapper.ListPersonToListGetAllPersonsDTO(getAll);
    }

    public async Task<GetOnePersonDTO> GetOnePerson(string id)
    {
        var getOne = await _peopleRepository.GetById(id);
        return _mapper.PersonToGetOnePersonDTO(getOne);
    }

    public async Task<UpdatePersonDTO> UpdatePerson(string id, AlterPersonDTO person)
    {
        var theActor = _mapper.AlterPersonDTOToPerson(person);
        theActor.Id = id;
        await _peopleRepository.Update(theActor);
        var updatedPerson = await _peopleRepository.GetById(theActor.Id);
        return _mapper.PersonToUpdatePersonDTO(updatedPerson);
    }

    public async Task<UpdatePersonDTO> AddPerson(AlterPersonDTO person)
    {
        var addedPerson = await _peopleRepository.Add(_mapper.AlterPersonDTOToPerson(person));
        return _mapper.PersonToUpdatePersonDTO(addedPerson);
    }

    public async Task<bool> DeletePerson(string id)
    {
        var entity = await _peopleRepository.GetById(id) ?? throw new NotImplementedException($"No entity found with id {id}");

        return await _peopleRepository.Delete(entity);
    }

    public Task<List<ActorBy>> FindActorsByName(string name)
    {
        return _peopleRepository.FindActorsByName(name) ?? throw new NullReferenceException("No actors found");
    }

    public Task<List<ActorBy>> FindActorsByMovie(string movieId)
    {
        return _peopleRepository.FindActorsByMovie(movieId) ?? throw new NullReferenceException("No actors found");
    }
    

    public Task<List<PopularCoPlayer>> GetPopularCoPlayers(string actorName)
    {
        return _peopleRepository.GetPopularCoPlayers(actorName);
    }
    
}