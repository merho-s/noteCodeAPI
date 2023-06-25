using Microsoft.EntityFrameworkCore;
using noteCodeAPI.Models;
using noteCodeAPI.Tools;

namespace noteCodeAPI.Repositories
{
    public class TagAliasRepository : BaseRepository<TagAlias>
    {
        public TagAliasRepository(DataDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<bool> DeleteAsync(TagAlias element)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<TagAlias>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<TagAlias> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> SaveAsync(TagAlias element)
        {
            throw new NotImplementedException();
        }

        public async Task<TagAlias> GetAliasByTagIdAsync(int tagId)
        {
            return await _dbContext.TagAliases.FirstOrDefaultAsync(a => a.CodetagId == tagId);
        }

        public async Task<TagAlias> GetAliasByNameAsync(string name)
        {
            return await _dbContext.TagAliases.FirstOrDefaultAsync(a => a.Name == name.ToLower());
        }
    }
}
