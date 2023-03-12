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

        public override bool Delete(Codetag element)
        {
            throw new NotImplementedException();
        }

        public override List<Codetag> GetAll()
        {
            return _dbContext.Codetags.Include(t => t.Aliases).ToList();
        }

        public override Codetag GetById(int id)
        {
            return _dbContext.Codetags.Include(t => t.Aliases).FirstOrDefault(t => t.Id == id);
        }

        public override bool Save(Codetag element)
        {
            throw new NotImplementedException();
        }

        public Codetag GetByName(string name)
        {
            return _dbContext.Codetags.Include(t => t.Aliases).FirstOrDefault(t => t.Name.ToLower() == name.ToLower());
        }

        public Codetag GetByAliasName(string alias)
        {
            return _dbContext.Codetags.Include(t => t.Aliases).FirstOrDefault(t => t.Aliases.Contains(_tagRepos.GetAliasByName(alias)));
        }
    }
}
