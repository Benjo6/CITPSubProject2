namespace DataLayer.Generics;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(string id);
    Task<List<T>> GetAll(int? page = 1, int? perPage = 10);
    Task<T> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(T entity);
}
