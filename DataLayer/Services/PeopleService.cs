using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class PeopleService : IPeopleService
{
    private IPeopleRepository _peopleRepository;
    private ObjectMapper _mapper;
    

    public PeopleService(IPeopleRepository peopleRepository)
    {
        _peopleRepository = peopleRepository;
        _mapper = new ObjectMapper();
    }

    public async Task<List<GetAllPersonDTO>> GetAllPerson()
    {
        var getAll = await _repository.GetAll();
        return _mapper.GetAllPersonDTO(getAll) ?? new List<GetAllPersonDTO>();
    }

    public async Task<GetOnePersonDTO> GetOnePerson(string id)
    {
        var getOne = await _repository.GetById(id);
        return _mapper.GetOnePersonDTO(getOne);
    }

    public async Task<UpdatePersonDTO> UpdatePerson(string id, AlterPersonDTO person)
    {
        _ = await _repository.GetById(id);
        var old  = _mapper.AlterPersonDTO(person);
        old.Id = id;
        await _repository.Update(ep);
        var updated = await _repository.GetById(old.Id);
        return _mapper.UpdatePersonDTO(updated);
    }

    public async Task<GetOnePersonDTO> AddPerson(AlterPersonDTO person)
    {
        var added = await _repository.Add(_mapper.AlterPersonDTO(person));
        return _mapper.GetOnePersonDTO(added);
    }

    public async Task<bool> DeletePerson(string id)
    {
        var entity = await _repository.GetById(id) ?? throw new KeyNotFoundException($"No entity found with id {id}");

        return await _repository.Delete(entity);
    }
}