using Common.DataTransferObjects;
using Common.Domain;

namespace DataLayer.Services.Contracts;

public interface IPeopleService
{
    Task<List<GetAllPersonDTO>> GetAllPerson();
    Task<GetOnePersonDTO> GetOnePerson(string id);
    Task<UpdatePersonDTO> UpdatePerson(string id, AlterPersonDTO person);
    Task<Person> AddPerson(AlterPersonDTO person);
    Task<bool> DeletePerson(string id);
}