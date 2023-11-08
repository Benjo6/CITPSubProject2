using DataLayer.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Generics;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T> Add(T entity)
    {
        _ = _context.Set<T>().Add(entity);
        _ = await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(T entity)
    {
        _ = _context.Set<T>().Remove(entity);
        _ = await _context.SaveChangesAsync();
        return true;

    }

    public async Task<List<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(string id)
    {
        return await _context.Set<T>().FindAsync(id) ?? throw new KeyNotFoundException($"No entity found with id {id}"); ;
    }

    public async Task<bool> Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
}
