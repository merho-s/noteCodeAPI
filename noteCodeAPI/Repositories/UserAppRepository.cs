using Microsoft.EntityFrameworkCore;
using noteCodeAPI.Models;
using noteCodeAPI.Tools;

namespace noteCodeAPI.Repositories
{
    public class UserAppRepository : BaseRepository<UserApp>
    {
        public UserAppRepository(DataDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<bool> DeleteAsync(UserApp element)
        {
            _dbContext.Users.Remove(element);
            return await UpdateAsync();
        }

        public override async Task<List<UserApp>> GetAllAsync()
        {
            return _dbContext.Users.Include(u => u.Notes).ToList();
        }

        public override async Task<UserApp> GetByIdAsync(int id)
        {
            return _dbContext.Users.Include(u => u.Notes).FirstOrDefault(u => u.Id == id);
        }

        public override async Task<bool> SaveAsync(UserApp element)
        {
            _dbContext.Users.Add(element);
            return await UpdateAsync();
        }

        public async Task<UserApp> SearchOneAsync(Func<UserApp, bool> searchMethod)
        {
            return _dbContext.Users.Include(u => u.Notes).FirstOrDefault(searchMethod);
        }
    }
}
