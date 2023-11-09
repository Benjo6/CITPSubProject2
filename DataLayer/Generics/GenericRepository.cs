using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataLayer.Generics;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> Add(T entity)
    {
        _ = _dbSet.Add(entity);
        _ = await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<T>> GetAll(int? page = 1, int? perPage = 10)
    {
        return await _dbSet.AsNoTracking().Skip((page-1)*perPage ?? 0).Take(perPage ?? 10).ToListAsync();
    }

    public async Task<T> GetById(string id)
    {
        return await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"No entity found with id {id}"); ;
    }

    public async Task<bool> Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
}
