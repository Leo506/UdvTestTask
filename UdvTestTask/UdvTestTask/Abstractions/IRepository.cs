namespace UdvTestTask.Abstractions;

public interface IRepository<in T>
{
    Task AddAsync(T entity);
}