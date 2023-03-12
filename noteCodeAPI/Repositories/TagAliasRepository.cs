using noteCodeAPI.Models;
using noteCodeAPI.Tools;

namespace noteCodeAPI.Repositories
{
    public class TagAliasRepository : BaseRepository<TagAlias>
    {
        public TagAliasRepository(DataDbContext dbContext) : base(dbContext)
        {
        }

        public override bool Delete(TagAlias element)
        {
            throw new NotImplementedException();
        }

        public override List<TagAlias> GetAll()
        {
            throw new NotImplementedException();
        }

        public override TagAlias GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Save(TagAlias element)
        {
            throw new NotImplementedException();
        }

        public TagAlias GetAliasByTagId(int tagId)
        {
            return _dbContext.TagAliases.FirstOrDefault(a => a.CodetagId == tagId);
        }

        public TagAlias GetAliasByName(string name)
        {
            return _dbContext.TagAliases.FirstOrDefault(a => a.Name == name);
        }
    }
}
