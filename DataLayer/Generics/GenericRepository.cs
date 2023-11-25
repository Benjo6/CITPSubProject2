using DataLayer.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Common;
using Common.Utils;

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

    public async Task<List<T>> GetAll(Filter filter)
    {
        var query = _dbSet.AsNoTracking();

        // Apply each filter condition
        query = filter.Conditions.Select(condition => ExpressionUtils.GetFilterExpression<T>(condition)).Aggregate(query, (current, filterExpression) => current.Where(filterExpression));

        // Apply sorting
        if (string.IsNullOrEmpty(filter.SortBy))
            return await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        
        var orderByExpression = ExpressionUtils.GetPropertyExpression<T>(filter.SortBy);
        query = filter.IsAscending ? query.OrderBy(orderByExpression) : query.OrderByDescending(orderByExpression);

        // Apply pagination
        return await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
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
