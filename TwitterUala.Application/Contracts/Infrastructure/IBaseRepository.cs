namespace TwitterUala.Application.Contracts.Infrastructure
{
    public interface IBaseRepository<T> where T : class
    {
        Task Add(T entity);
    }
}
