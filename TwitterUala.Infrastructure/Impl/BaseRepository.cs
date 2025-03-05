using Microsoft.EntityFrameworkCore;
using TwitterUala.Application.Contracts.Infrastructure;

namespace TwitterUala.Infrastructure.Impl
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbSet<T> _entity;
        protected readonly DbContext _context;
        public BaseRepository(DbContext context)
        {
            _entity = context.Set<T>();
        }

        public virtual async Task Add(T entity) => await _entity.AddAsync(entity);
    }
}
