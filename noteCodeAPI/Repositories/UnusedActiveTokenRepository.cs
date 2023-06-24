using noteCodeAPI.Models;
using noteCodeAPI.Tools;

namespace noteCodeAPI.Repositories
{
    public class UnusedActiveTokenRepository : BaseRepository<UnusedActiveToken>
    {
        public UnusedActiveTokenRepository(DataDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<bool> DeleteAsync(UnusedActiveToken element)
        {
            _dbContext.UnusedActiveTokens.Remove(element);
            return await UpdateAsync();
        }

        public override async Task<List<UnusedActiveToken>> GetAllAsync()
        {
            return _dbContext.UnusedActiveTokens.ToList();
        }

        public override async Task<UnusedActiveToken> GetByIdAsync(int id)
        {
            return _dbContext.UnusedActiveTokens.FirstOrDefault(t => t.Id == id);
        }

        public override async Task<bool> SaveAsync(UnusedActiveToken element)
        {
            _dbContext.UnusedActiveTokens.Add(element);
            return await UpdateAsync();
        }
    }
}
