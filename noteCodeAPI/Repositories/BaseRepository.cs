using noteCodeAPI.Tools;

namespace noteCodeAPI.Repositories
{
    public abstract class BaseRepository<T>
    {
        protected DataDbContext _dbContext;

        protected BaseRepository(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public abstract Task<bool> SaveAsync(T element);
        public abstract Task<bool> DeleteAsync(T element);
        public abstract Task<T> GetByIdAsync(int id);
        public abstract Task<List<T>> GetAllAsync();
        public async Task<bool> UpdateAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
