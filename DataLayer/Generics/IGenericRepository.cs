﻿namespace DataLayer.Generics;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(int id);
    Task<List<T>> GetAll();
    Task<T> Add(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(int id);
}
