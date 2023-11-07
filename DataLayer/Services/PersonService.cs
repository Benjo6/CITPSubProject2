using DataLayer.Models;
using DataLayer.Repositories.Contracts;
using DataLayer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository<Person> _repository;

    public PersonService(IPersonRepository<Person> repository)
    {
        _repository = repository;
    }

    public async Task<Person> AddPerson(string name, DateTime? birthdate, DateTime? deathdate, string profession, string knownfor)
    {
        var person = new Person { Name = name, BirthDate=birthdate, DeathDate=deathdate, Profession=profession, KnownFor=knownfor };
        return await _repository.Add(person);
    }

    public async Task<bool> DeletePerson(string name)
    {
        if (await _repository.GetByName(name) == null)
        {
            return false;
        }
        return await _repository.DeleteAsync(name);
    }

    public async Task<List<Person>> GetAllAsync()
    {
        return (List<Person>)await _repository.GetAll();
    }

    public async Task<Person> GetByName(string name)
    {
        return await _repository.GetByName(name);
    }

    public async Task<bool> UpdatePerson(string id, string name, DateTime? birthdate, DateTime? deathdate, string profession, string knownfor)
    {
        var person = await _repository.GetByName(id);
        if (person == null)
        {
            return false;
        }

        person.Name = name;
        person.BirthDate = birthdate;
        person.DeathDate = deathdate;
        person.Profession = profession;
        person.KnownFor = knownfor;

        return await _repository.Update(person);
    }
}
