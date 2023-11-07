using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services.Contracts;

public interface IPersonService
{
    Task<Person> GetByName(string name);
    Task<List<Person>> GetAllAsync();
    Task<Person> AddPerson(string name, DateTime? birthdate, DateTime? deathdate, string profession, string knownfor);
    Task<bool> UpdatePerson(string id, string name, DateTime? birthdate, DateTime? deathdate, string profession, string knownfor);
    Task<bool> DeletePerson(string id);

}
