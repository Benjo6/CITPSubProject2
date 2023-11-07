using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Contracts;

internal interface IPersonRepository<Person> where Person : class
{
    Task<Person> GetByName(string name);
    Task<IEnumerable<Person>> GetAll();
    Task<Person> Add(Person person);
    Task<bool> Update(Person person);
    Task<bool> DeleteAsync(string id);
}
