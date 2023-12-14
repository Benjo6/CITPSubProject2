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
        IQueryable<T> query = _dbSet;

        // Apply flexible filtering using FilterUtils
        if (filter != null && filter.FilterCriteria.Any())
        {
            query = FilterUtils.ApplyFilter(query, filter);
        }

        // Validate pagination parameters
        var pageNumber = filter?.PageNumber > 0 ? filter.PageNumber : 1;
        var pageSize = filter?.PageSize > 0 ? filter.PageSize : 10;

        // Apply sorting and pagination
        if (!string.IsNullOrEmpty(filter?.SortBy))
        {
            var orderByExpression = ExpressionUtils.GetPropertyExpression<T>(filter.SortBy);
            query = filter.IsAscending ? query.OrderBy(orderByExpression) : query.OrderByDescending(orderByExpression);
        }
        var pagedResult = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return pagedResult;
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