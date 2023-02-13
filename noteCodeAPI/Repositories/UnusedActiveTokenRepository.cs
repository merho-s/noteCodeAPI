using noteCodeAPI.Models;
using noteCodeAPI.Tools;

namespace noteCodeAPI.Repositories
{
    public class UnusedActiveTokenRepository : BaseRepository<UnusedActiveToken>
    {
        public UnusedActiveTokenRepository(DataDbContext dbContext) : base(dbContext)
        {
        }

        public override bool Delete(UnusedActiveToken element)
        {
            _dbContext.UnusedActiveTokens.Remove(element);
            return Update();
        }

        public override List<UnusedActiveToken> GetAll()
        {
            return _dbContext.UnusedActiveTokens.ToList();
        }

        public override UnusedActiveToken GetById(int id)
        {
            return _dbContext.UnusedActiveTokens.FirstOrDefault(t => t.Id == id);
        }

        public override bool Save(UnusedActiveToken element)
        {
            _dbContext.UnusedActiveTokens.Add(element);
            return Update();
        }
    }
}
