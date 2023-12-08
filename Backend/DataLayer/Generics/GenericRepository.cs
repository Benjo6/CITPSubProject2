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

    public async Task<(List<T>,Metadata)> GetAll(Filter filter)
    {
        IQueryable<T> query = _dbSet;
        var metaData = new Metadata();

        // Apply flexible filtering
        foreach (var criteria in filter.FilterCriteria)
        {
            var propertyInfo = typeof(T).GetProperty(criteria.Key);
            if (propertyInfo != null)
            {
                query = query.Where(t => EF.Property<string>(t, criteria.Key).Contains(criteria.Value));
            }
        }
        
        // Get total count before pagination
        metaData.TotalCount = await query.CountAsync();

        // Apply sorting
        if (!string.IsNullOrEmpty(filter.SortBy))
        {
            var orderByExpression = ExpressionUtils.GetPropertyExpression<T>(filter.SortBy);
            query = filter.IsAscending ? query.OrderBy(orderByExpression) : query.OrderByDescending(orderByExpression);
        }

        // Apply pagination and convert to List
        var pagedResult = await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

        return (pagedResult,metaData);
        
    }


    public async Task<T> GetById(string id)
    {
        return await _dbSet.FindAsync(id) ?? throw new KeyNotFoundException($"No entity found with id {id}");
        ;
    }

    public async Task<bool> Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
}