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

        public abstract bool Save(T element);
        public abstract bool Delete(T element);
        public abstract T GetById(int id);
        public abstract List<T> GetAll();
        public bool Update()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}
