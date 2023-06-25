using Microsoft.EntityFrameworkCore;
using noteCodeAPI.Models;
using noteCodeAPI.Tools;

namespace noteCodeAPI.Repositories
{
    public class CodetagRepository : BaseRepository<Codetag>
    {
        private TagAliasRepository _tagRepos;
        public CodetagRepository(DataDbContext dbContext, TagAliasRepository tagRepos) : base(dbContext)
        {
            _tagRepos = tagRepos;
        }

        public override async Task<bool> DeleteAsync(Codetag element)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Codetag>> GetAllAsync()
        {
            return await _dbContext.Codetags.Include(t => t.Aliases).ToListAsync();
        }

        public override async Task<Codetag> GetByIdAsync(int id)
        {
            return await _dbContext.Codetags.Include(t => t.Aliases).FirstOrDefaultAsync(t => t.Id == id);
        }

        public override async Task<bool> SaveAsync(Codetag element)
        {
            throw new NotImplementedException();
        }

        public async Task<Codetag> GetByNameAsync(string name)
        {
            return await _dbContext.Codetags.Include(t => t.Aliases).FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }

        public async Task<Codetag> GetByAliasNameAsync(string alias)
        {
            var tagAlias = await _tagRepos.GetAliasByNameAsync(alias);
            return await _dbContext.Codetags.Include(t => t.Aliases).FirstOrDefaultAsync(t => t.Aliases.Contains(tagAlias));
        }
    }
}
