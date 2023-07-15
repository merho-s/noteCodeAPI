using Microsoft.EntityFrameworkCore;
using noteCodeAPI.Models;
using noteCodeAPI.Tools;

namespace noteCodeAPI.Repositories
{
    public class WaitingUserRepository : BaseRepository<WaitingUser>
    {
        public WaitingUserRepository(DataDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<bool> DeleteAsync(WaitingUser element)
        {
            _dbContext.WaitingUsers.Remove(element);
            return await UpdateAsync();
        }

        public override async Task<List<WaitingUser>> GetAllAsync()
        {
            return await _dbContext.WaitingUsers.ToListAsync();
        }

        public override async Task<WaitingUser> GetByIdAsync(int id)
        {
            return await _dbContext.WaitingUsers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<bool> SaveAsync(WaitingUser element)
        {
            _dbContext.WaitingUsers.Add(element);
            return await UpdateAsync();
        }
    }
}
