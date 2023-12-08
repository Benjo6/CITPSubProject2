using Common;

namespace DataLayer.Generics;

public interface IGenericRepository<T>
{
    Task<T> GetById(string id);
    Task<(List<T>, Metadata)> GetAll(Filter filter);
    Task<T> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(T entity);
}
