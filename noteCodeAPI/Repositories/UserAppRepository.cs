using Microsoft.EntityFrameworkCore;
using noteCodeAPI.Models;
using noteCodeAPI.Tools;

namespace noteCodeAPI.Repositories
{
    public class UserAppRepository : BaseRepository<UserApp>
    {
        public UserAppRepository(DataDbContext dbContext) : base(dbContext)
        {
        }

        public override bool Delete(UserApp element)
        {
            _dbContext.Users.Remove(element);
            return Update();
        }

        public override List<UserApp> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public override UserApp? GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public override bool Save(UserApp element)
        {
            _dbContext.Users.Add(element);
            return Update();
        }

        public UserApp SearchOne(Func<UserApp, bool> searchMethod)
        {
            return _dbContext.Users.FirstOrDefault(searchMethod);
        }
    }
}
