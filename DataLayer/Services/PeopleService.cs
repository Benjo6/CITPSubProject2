using Common.DataTransferObjects;
using Common.Domain;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;

namespace DataLayer.Services;

public class PeopleService : IPeopleService
{
    private IPeopleRepository _peopleRepository;

    public PeopleService(IPeopleRepository peopleRepository)
    {
        _peopleRepository = peopleRepository;
    }

    public async Task<List<GetAllPersonDTO>> GetAllPerson()
    {
        throw new NotImplementedException();
    }

    public async Task<GetOnePersonDTO> GetOnePerson(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<UpdatePersonDTO> UpdatePerson(string id, AlterPersonDTO person)
    {
        throw new NotImplementedException();
    }

    public async Task<Person> AddPerson(AlterPersonDTO person)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeletePerson(string id)
    {
        throw new NotImplementedException();
    }
}