using Microsoft.EntityFrameworkCore;
using noteCodeAPI.Models;
using noteCodeAPI.Tools;

namespace noteCodeAPI.Repositories
{
    public class CodetagRepository : BaseRepository<Codetag>
    {
        public CodetagRepository(DataDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<bool> DeleteAsync(Codetag element)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Codetag>> GetAllAsync()
        {
            return await _dbContext.Codetags.ToListAsync();
        }

        public override async Task<Codetag> GetByIdAsync(int id)
        {
            return await _dbContext.Codetags.FirstOrDefaultAsync(t => t.Id == id);
        }

        public override async Task<bool> SaveAsync(Codetag element)
        {
            throw new NotImplementedException();
        }

        public async Task<Codetag> GetByNameAsync(string name)
        {
            return await _dbContext.Codetags.FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }

        public override Task<Codetag> GetByGuidAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
