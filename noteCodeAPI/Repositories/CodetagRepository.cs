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
            return _dbContext.Codetags.Include(t => t.Aliases).ToList();
        }

        public override async Task<Codetag> GetByIdAsync(int id)
        {
            return _dbContext.Codetags.Include(t => t.Aliases).FirstOrDefault(t => t.Id == id);
        }

        public override async Task<bool> SaveAsync(Codetag element)
        {
            throw new NotImplementedException();
        }

        public async Task<Codetag> GetByNameAsync(string name)
        {
            return _dbContext.Codetags.Include(t => t.Aliases).FirstOrDefault(t => t.Name.ToLower() == name.ToLower());
        }

        public async Task<Codetag> GetByAliasNameAsync(string alias)
        {
            return _dbContext.Codetags.Include(t => t.Aliases).FirstOrDefaultAsync(async t => t.Aliases.Contains(await _tagRepos.GetAliasByNameAsync(alias)));
        }
    }
}
