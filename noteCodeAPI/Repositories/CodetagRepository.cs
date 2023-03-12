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

        public Codetag GetByAlias(TagAlias alias)
        {
            return _dbContext.Codetags.Include(t => t.Aliases).FirstOrDefault(t => t.Aliases.Contains(alias));
        }
    }
}
