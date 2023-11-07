using DataLayer.Models;
using DataLayer.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories;

internal class PersonRepository<Person> : IPersonRepository<Person> where Person : class
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger _logger;

    public PersonRepository(AppDbContext appDbContext, ILogger logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    public async Task<Person> Add(Person person)
    {
        _appDbContext.Set<Person>().Add(person);
        await _appDbContext.SaveChangesAsync();
        return person;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var person = await GetByName(id);

        if (person != null) { return false; }

        _appDbContext.Set<Movie>().Remove(person);
        await _appDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Person>> GetAll()
    {
        return await _appDbContext.Set<Person>().ToListAsync();
    }

    public async Task<Person> GetByName(string name)
    {
        return await _appDbContext.Set<Person>().FindAsync(name);
    }

    public async Task<bool> Update(Person person)
    {
        _appDbContext.Entry(person).State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}
